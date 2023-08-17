using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stage;
    public Transform playerPos;

    public GameObject player;
    //public GameObject gameOverSet;
    [SerializeField]
    UpgradePanelManager upgradePanelManager;


    void Awake()
    {
        StageStart();
    }
    
    void Update()
    {

    }
    public void StageStart()
    {

       // ReadSpawnFile();
    }
    public void StageEnd()
    {
        // Player Repos
        player.transform.position = playerPos.position;
        // 스테이지 증가
        stage++;
        if (stage > 3)
        {
            Invoke("GameOver", 2);
        }
        else
        {
            if (GameManager.instance.upgradeController.selectedUpgrades == null)
            {
                GameManager.instance.upgradeController.selectedUpgrades = new List<UpgradeData>();
            }
            GameManager.instance.upgradeController.selectedUpgrades.Clear();
            GameManager.instance.upgradeController.selectedUpgrades.AddRange(GameManager.instance.upgradeController.GetUpgrades(3));

            upgradePanelManager.OpenPanel(GameManager.instance.upgradeController.selectedUpgrades);
            Invoke("StageStart", 2);
        }
    }

    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = GameManager.instance.upgradeController.selectedUpgrades[selectedUpgradeId];

        GameManager.instance.upgradeController.acquiredUpgrades.Add(upgradeData);
        GameManager.instance.upgradeController.ApplyUpgrade(upgradeData);
    }
    public void GameOver()
    {
        //gameOverSet.SetActive(true);
    }
    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }
    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);
    }
}
