using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public Transform playerPos;

    public GameObject player;
    //public GameObject gameOverSet;



    void Awake()
    {

    }
    
    void Update()
    {

    }
    
    
    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName);
    }

    public void GoNextStoryAct(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    public void GoNextAct()
    {
        
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
