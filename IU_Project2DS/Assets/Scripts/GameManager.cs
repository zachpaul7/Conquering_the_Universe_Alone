using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int stage;
    public Transform playerPos;
    public Transform[] spawnPoints;
    public string[] enemyObjs;

    public Player playerInstance;
    [SerializeField]
    UpgradeController upgradeController;

    public GameObject player;
    public GameObject gameOverSet;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public List<Spawn> spawnList;
    public int spawnIndex;
    public bool spawnEnd;

    [SerializeField]
    UpgradePanelManager upgradePanelManager;
    [HideInInspector]
    public List<UpgradeData> selectedUpgrades;
    [SerializeField]
    public List<UpgradeData> acquiredUpgrades;
    


    void Awake()
    {
        spawnList = new List<Spawn>();
        enemyObjs = new string[] { "Enemy 0", "Enemy 1", "Enemy 2" };
        StageStart();
    }

    void ReadSpawnFile()
    {
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            if (line == null)
                break;

            Debug.Log(line);

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnList.Add(spawnData);
        }

        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }
    void Update()
    {
        curSpawnDelay += Time.deltaTime;

        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
    }

    public void StageStart()
    {
        ReadSpawnFile();
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
            if (selectedUpgrades == null)
            {
                selectedUpgrades = new List<UpgradeData>();
            }
            selectedUpgrades.Clear();
            selectedUpgrades.AddRange(upgradeController.GetUpgrades(3));

            upgradePanelManager.OpenPanel(selectedUpgrades);
            Invoke("StageStart", 2);
        }
    }
    public void Upgrade(int selectedUpgradeId)
    {
        UpgradeData upgradeData = selectedUpgrades[selectedUpgradeId];

        acquiredUpgrades.Add(upgradeData);
        upgradeController.ApplyUpgrade(upgradeData);
    }
    void SpawnEnemy()
    {
        if (spawnEnd)
        {
            return;
        }
        int enemyIndex = 0;
        switch (spawnList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 0;
                break;
            case "L":
                enemyIndex = 1;
                break;
            case "M":
                enemyIndex = 2;
                break;
        }

        int enemyPoint = spawnList[spawnIndex].point;

        GameObject prefab = Resources.Load<GameObject>(enemyObjs[enemyIndex]);
        GameObject enemy = Instantiate(prefab);

        enemy.transform.position = spawnPoints[enemyPoint].position;

        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player;
        enemyLogic.gameManager = this;

        if (enemyPoint == 5 || enemyPoint == 6)
        {
            enemy.transform.Rotate(Vector3.forward * 45);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
        else if (enemyPoint == 7 || enemyPoint == 8)
        {
            enemy.transform.Rotate(Vector3.back * 45);
            rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        }
        else
        {
            rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        }

        spawnIndex++;
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    public void GameOver()
    {
        gameOverSet.SetActive(true);
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
