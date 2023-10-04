using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeController : MonoBehaviour
{
    public GameObject player;
    public List<UpgradeData> upgrades;
    private Dictionary<string, bool> weaponUnlockStatus;

    [HideInInspector]
    public List<UpgradeData> selectedUpgrades;
    [SerializeField]
    public List<UpgradeData> acquiredUpgrades;

    [SerializeField] GameObject weapons;

    [SerializeField] GameObject rocket;
    [SerializeField] GameObject razer;
    [SerializeField] GameObject canon;

    public bool isRocket;
    private bool isCanon = false;
    private bool isRazer = false;

    private void Awake()
    {
        weaponUnlockStatus = new Dictionary<string, bool>()
        {
            { "Razer", false },
            { "Boom", false },
            { "Rocket", false },
        };

        isRocket = false;
        isCanon = false;
        isRazer = false;
    }

    private void FixedUpdate()
    {
        player = GameManager.instance.player;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // UpgradeController.cs

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int buildIndex = scene.buildIndex;

        if ((buildIndex == 2 || buildIndex == 4 || buildIndex == 6) && GameManager.instance.isRocketUnlocked)
        {
            UnlockRocket();
            GameManager.instance.isRocketUnlocked = false; // ȣ��Ǿ����� ǥ��
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        for (int i = 0; i < count;)
        {
            List<UpgradeData> tempAvailableUpgrades = new List<UpgradeData>(upgrades);
            tempAvailableUpgrades.RemoveAll(u => upgradeList.Contains(u)
                || acquiredUpgrades.Contains(u)
                || upgradeList.Any(us => us.Name == u.Name)
                || (u.upgradeType == UpgradeType.WeaponUpgrade && !acquiredUpgrades.Any(au => au.Name == u.Name && au.upgradeType == UpgradeType.WeaponUnlock))
                || (u.upgradeType == UpgradeType.WeaponUnlock && acquiredUpgrades.Any(au => au.Name == u.Name && au.upgradeType == UpgradeType.WeaponUnlock)));

            if (tempAvailableUpgrades.Count == 0)
            {
                Debug.LogWarning("Not enough unique upgrades to choose from");
                break;
            }

            int selectedIndex = Random.Range(0, tempAvailableUpgrades.Count);
            UpgradeData selectedUpgrade = tempAvailableUpgrades.ElementAt(selectedIndex);
            upgradeList.Add(selectedUpgrade);
            i++;

            if (selectedUpgrade.upgradeType == UpgradeType.WeaponUnlock)
            {
                weaponUnlockStatus[selectedUpgrade.Name] = true;
            }
        }

        return upgradeList;
    }

    public void ApplyUpgrade(UpgradeData upgradeData)
    {
        Debug.Log("Entering ApplyUpgrade function");

        if (acquiredUpgrades == null)
        {
            acquiredUpgrades = new List<UpgradeData>();
        }

        // Weapon Options
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                switch (upgradeData.Name)
                {
                    case "Raser":
                        UpgradeRazer();
                        break;
                    case "Canon":
                        Debug.Log("Entered Weapon Upgrade case for Canon");
                        break;
                    case "Rocket":
                        Debug.Log("Entered Weapon Upgrade case for Rocket");
                        break;
                }
                break;
            case UpgradeType.WeaponUnlock:
                switch (upgradeData.Name)
                {
                    case "Raser":

                        Debug.Log("Entered Weapon Upgrade case for Raser");
                        break;
                    case "Canon":
                        Debug.Log("Entered Weapon Upgrade case for Canon");
                        break;
                    case "Rocket":
                        Debug.Log("Entered Weapon Upgrade case for Rocket");
                        UnlockRocket();
                        break;
                }
                break;
            case UpgradeType.StatsUpgrade:
                switch (upgradeData.Name)
                {
                    case "Health":
                        Debug.Log("Entered Stats Upgrade case for Health");
                        PlusHealth();
                        break;
                    case "PowerUP":
                        Debug.Log("Entered Stats Upgrade case for PowerUP");
                        PowerUpgrade();
                        break;
                }
                break;
        }
    }
    private void UnlockRocket()
    {
        rocket = GameObject.Find("Main Ship").transform.GetChild(0).transform.GetChild(1).gameObject;

        if (rocket.activeSelf == false)
        {
            rocket.SetActive(true);
        }
    }

    private void UpgradeRazer()
    {
        // ���� Ȱ��ȭ�� Razer ���� ���׷��̵� ����

    }

    void PlusHealth()
    {
        Debug.Log("Entered Weapon Upgrade case");
        GameManager.instance.playerMaxHealth++;
        
    }

    void PowerUpgrade()
    {
        Debug.Log("Entered Weapon Upgrade case");
        GameManager.instance.playerPower++;

    }
}

public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
