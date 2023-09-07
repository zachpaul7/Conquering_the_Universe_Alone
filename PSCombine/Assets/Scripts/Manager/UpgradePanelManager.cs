using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private List<UpgradeButton> upgradeButtons;
    [SerializeField] private List<WeaponButton> wpButtons;

    PauseManager pauseManager;

    private int selectedOptionIndex = 0;
    private List<UpgradeData> currentUpgradeDatas;

    void Awake()
    {
        pauseManager = GetComponent<PauseManager>();

        upgradePanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;

        // Upgrade Buttons를 설정합니다.
        upgradeButtons = new List<UpgradeButton>();
        upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 1").GetComponent<UpgradeButton>());
        upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 2").GetComponent<UpgradeButton>());
        upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 3").GetComponent<UpgradeButton>());

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
            upgradePanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;

            upgradeButtons = new List<UpgradeButton>();
            upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 1").GetComponent<UpgradeButton>());
            upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 2").GetComponent<UpgradeButton>());
            upgradeButtons.Add(upgradePanel.transform.Find("Upgrade Button 3").GetComponent<UpgradeButton>());

            wpButtons = new List<WeaponButton>();
            wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(0).GetComponent<WeaponButton>());
            wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(1).GetComponent<WeaponButton>());
            wpButtons.Add(GameObject.Find("Weapon Buttons").transform.GetChild(2).GetComponent<WeaponButton>());
        }
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        Clean();
        pauseManager.PauseGame();
        upgradePanel.SetActive(true);
        currentUpgradeDatas = upgradeDatas;

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].gameObject.SetActive(true);
            upgradeButtons[i].Set(upgradeDatas[i]);
        }
    }
    public void Clean()
    {
        for (int i = 0; i < upgradeButtons.Count; i++)
        {
            upgradeButtons[i].Clean();
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

    private void UpdateWeaponButtonAfterUpgrade(int buttonIndex)
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

    private int GetNextAvailableWeaponButtonIndex()
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
    }
}
