using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeController : MonoBehaviour
{
    public List<UpgradeData> upgrades;
    private Dictionary<string, bool> weaponUnlockStatus;
    public GameManager gameManager;

    private void Awake()
    {
        weaponUnlockStatus = new Dictionary<string, bool>()
        {
            { "Razer", false },
            { "Boom", false },
            { "Rocket", false },
        };
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        for (int i = 0; i < count;)
        {
            List<UpgradeData> tempAvailableUpgrades = new List<UpgradeData>(upgrades);
            tempAvailableUpgrades.RemoveAll(u => upgradeList.Contains(u)
                || upgradeList.Any(us => us.Name == u.Name)
                || (u.upgradeType == UpgradeType.WeaponUpgrade && !gameManager.acquiredUpgrades.Any(au => au.Name == u.Name && au.upgradeType == UpgradeType.WeaponUnlock))
                || (u.upgradeType == UpgradeType.WeaponUnlock && gameManager.acquiredUpgrades.Any(au => au.Name == u.Name && au.upgradeType == UpgradeType.WeaponUnlock)));

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
        if (gameManager.acquiredUpgrades == null)
        {
            gameManager.acquiredUpgrades = new List<UpgradeData>();
        }
        
        if (gameManager.acquiredUpgrades.Contains(upgradeData))
        {
            Debug.LogWarning("Upgrade already acquired: " + upgradeData);
            return;
        }
        gameManager.acquiredUpgrades.Add(upgradeData);

        // Weapon Options
        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUpgrade:
                switch (upgradeData.Name)
                {
                    case "Razer":
                        UpgradeRazer();
                        break;
                    case "Boom":
                        break;
                    case "Rocket":
                        break;
                }
                break;
            case UpgradeType.WeaponUnlock:
                switch (upgradeData.Name)
                {
                    case "Razer":
                        UnlockRazer();
                        break;
                    case "Boom":
                        break;
                    case "Rocket":
                        break;
                }
                break;
            case UpgradeType.StatsUpgrade:
                break;
        }

        upgrades.Remove(upgradeData);
    }
    private void UnlockRazer()
    {
        // Razer 무기 잠금 해제 로직

    }

    private void UpgradeRazer()
    {
        // 현재 활성화된 Razer 무기 업그레이드 로직

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
