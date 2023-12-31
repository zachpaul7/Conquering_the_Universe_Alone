using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private List<UpgradeButton> upgradeButtons;
    [SerializeField] private List<WeaponButton> wpButtons;
    [SerializeField] private List<UpgradeText> upgradeTexts;

    [SerializeField]private GameObject joy;
    PauseManager pauseManager;

    private int selectedOptionIndex = 0;
    private List<UpgradeData> currentUpgradeDatas;
    [HideInInspector] public int btnNum;


    void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        
        upgradePanel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
        

        // Upgrade Buttons를 설정합니다.
        upgradeButtons = new List<UpgradeButton>();
        upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 1").GetComponent<UpgradeButton>());
        upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 2").GetComponent<UpgradeButton>());
        upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 3").GetComponent<UpgradeButton>());

        // Upgrade Texts를 설정합니다.
        upgradeTexts = new List<UpgradeText>();
        upgradeTexts.Add(upgradePanel.transform.Find("Upgrade Button 1").GetComponent<UpgradeText>());
        upgradeTexts.Add(upgradePanel.transform.Find("Upgrade Button 2").GetComponent<UpgradeText>());
        upgradeTexts.Add(upgradePanel.transform.Find("Upgrade Button 3").GetComponent<UpgradeText>());

        // Weapon Buttons를 설정합니다.
        wpButtons = new List<WeaponButton>();
        wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(0).GetComponent<WeaponButton>());
        wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(1).GetComponent<WeaponButton>());
        wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(2).GetComponent<WeaponButton>());

    }

    private void Update()
    {
        if (upgradePanel == null)
        {
            upgradePanel = GameObject.Find("Canvas").transform.GetChild(2).gameObject;

            upgradeButtons = new List<UpgradeButton>();
            upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 1").GetComponent<UpgradeButton>());
            upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 2").GetComponent<UpgradeButton>());
            upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 3").GetComponent<UpgradeButton>());

            // Upgrade Texts를 설정합니다.
            upgradeTexts = new List<UpgradeText>();
            upgradeTexts.Add(upgradePanel.transform.Find("Upgrade Button 1").GetComponent<UpgradeText>());
            upgradeTexts.Add(upgradePanel.transform.Find("Upgrade Button 2").GetComponent<UpgradeText>());
            upgradeTexts.Add(upgradePanel.transform.Find("Upgrade Button 3").GetComponent<UpgradeText>());


            wpButtons = new List<WeaponButton>();
            wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(0).GetComponent<WeaponButton>());
            wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(1).GetComponent<WeaponButton>());
            wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(2).GetComponent<WeaponButton>());
        }
        if (joy == null && (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 6))
        {

            joy = GameObject.Find("Canvas1").transform.GetChild(1).gameObject;

        }

        
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        pauseManager.PauseGame();
        joy.SetActive(false);
        upgradePanel.SetActive(true);
        GameManager.instance.soundManager.PlaySFX("PowerUP");
        currentUpgradeDatas = upgradeDatas;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeTexts[i].gameObject.SetActive(true);

            upgradeButtons[i].Set(upgradeDatas[i]);
            upgradeTexts[i].Set(upgradeDatas[i]);
        }
    }
    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
            upgradeTexts[i].Clean();
        }
    }

    public void SelectOption(int index)
    {
        selectedOptionIndex = index;
    }
    public void Upgrade(int pressedButtonID)
    {
        if (pressedButtonID < 0 || pressedButtonID >= currentUpgradeDatas.Count) // 범위를 벗어난 값 체크
        {
            Debug.LogWarning("Invalid upgrade button ID: " + pressedButtonID);
            return;
        }

        GameManager.instance.stageManager.Upgrade(pressedButtonID);
        ClosePanel();
        UpdateWeaponButtonAfterUpgrade(pressedButtonID);
    }

    public void UpdateWeaponButtonAfterUpgrade(int buttonIndex)
    {
        if (buttonIndex >= 0 && buttonIndex < currentUpgradeDatas.Count)
        {
            UpgradeData selectedUpgradeData = currentUpgradeDatas[buttonIndex];

            if (selectedUpgradeData.upgradeType == UpgradeType.WeaponUnlock && selectedUpgradeData.wpIcon != null)
            {
                int weaponButtonIndex = GetNextAvailableWeaponButtonIndex();

                if (weaponButtonIndex >= 0 && weaponButtonIndex < wpButtons.Count)
                {
                    wpButtons[weaponButtonIndex].SetIcon(selectedUpgradeData);
                    wpButtons[weaponButtonIndex].gameObject.SetActive(true);
                }
            }
        }
    }

    public int GetNextAvailableWeaponButtonIndex()
    {
        for (int i = 0; i < wpButtons.Count; i++)
        {
            if (!wpButtons[i].gameObject.activeInHierarchy)
            {
                return i;
            }
        }

        return -1; // Return -1 if no available weapon button index is found
    }

    

    public void ClosePanel()
    {
        pauseManager.UnPauseGame();
        upgradePanel.SetActive(false);
        joy.SetActive(true);
    }
}
