using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    //public GameObject gameOverSet;

    public void Start()
    {
        player = GameManager.instance.player;
        gameManager = GameObject.Find("---GameManager---");
    }
    IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return YieldCache.WaitForSeconds(4f);
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


    public IEnumerator RespawnPlayer()
    {
        yield return YieldCache.WaitForSeconds(2f);

        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

    }


}
