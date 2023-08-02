using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    [SerializeField]
    GameObject upgradePanel;
    PauseManager pauseManager;

    [SerializeField]
    List<UpgradeButton> upgradeButtons;

    void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
    }

    public void OpenPanel(List<UpgradeData> upgradeDatas)
    {
        pauseManager.PauseGame();
        upgradePanel.SetActive(true);

        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upgradeButtons[i].Set(upgradeDatas[i]);
            upgradeButtons[i].GetComponent<Button>().onClick.AddListener(ClosePanel);
        }
    }
    public void Upgrade(int pressedButtonID)
    {
        Debug.Log("Player pressed : " + pressedButtonID.ToString());
    }
    public void ClosePanel()
    {
        pauseManager.UnPauseGame();
        upgradePanel.SetActive(false);
    }
}
