using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    public GameManager GameManager;
    [SerializeField]
    GameObject upgradePanel;
    PauseManager pauseManager;

    [SerializeField]
    List<UpgradeButton> upgradeButtons;
    [SerializeField]
    List<WeaponButton> wpButtons;

    private int selectedOptionIndex = 0;
    private List<UpgradeData> currentUpgradeDatas;
    void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
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

        GameManager.GetComponent<GameManager>().Upgrade(pressedButtonID);
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
