using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject player;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public List<Spawn> spawnList;
    public Transform[] spawnPoints;

    public int spawnIndex;
    public bool spawnEnd;

    public int stage;
    public UpgradePanelManager upgradePanelManager;

    void Awake()
    {
        // ���� ����Ʈ�� �� ������Ʈ �迭 �ʱ�ȭ
        spawnList = new List<Spawn>();
        StageStart();
        upgradePanelManager = GameObject.Find("---UpgradeController---").GetComponent<UpgradePanelManager>();
    }

    void Update()
    {
        // �ð��� ���� ���� ���� �����̸� ������Ʈ
        curSpawnDelay += Time.deltaTime;

        // ���� �����̰� �� �Ǿ��� ���� ������ ������ �ʾ����� ���� ����
        if (curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }
    }
    void CheckCompont()
    {
        if (upgradePanelManager == null)
        {
            upgradePanelManager = GameObject.Find("---UpgradeController---").GetComponent<UpgradePanelManager>();
        }

        
    }
    void StageStart()
    {
        ReadSpawnFile();

    }
    public void StageEnd()
    {
        // Player Repos
        player.transform.position = GameManager.instance.stageManager.playerPos.position;

        // �������� ����
        stage++;

        if (stage > 2)
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

    public void ReadSpawnFile()
    {
        // ���� ����Ʈ�� ���� ���� �ε����� ���� ���� ���� �ʱ�ȭ
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // ������ �ؽ�Ʈ ���Ϸκ��� ���� ������ �о����
        TextAsset textFile = Resources.Load("SpawnText/Act " + GameManager.instance.actNum + "/Stage " + stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        // �ؽ�Ʈ ������ �� ���� �а� �Ľ��Ͽ� ���� ��ü�� ����
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            // ������ ���� �����ߴ��� Ȯ��
            if (line == null)
                break;

            // ���ο� ���� ��ü�� �����ϰ� �Ӽ��� ä��
            Spawn spawnData = gameObject.AddComponent<Spawn>();

            spawnData.type = line.Split(',')[0];
            spawnData.point = int.Parse(line.Split(',')[1]);
            spawnData.delay = float.Parse(line.Split(',')[2]);

            spawnList.Add(spawnData);
        }

        // stringReader�� �ݰ� ���� ���� �����̸� ���� ����Ʈ�� ù ��° �׸����� ����
        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }

    void SpawnEnemy()
    {
        // ������ ����Ǿ����� Ȯ��
        if (spawnEnd)
        {
            return;
        }

        // ���� ����Ʈ���� ������ Ÿ�Կ� ���� enemyIndex ����
        int enemyIndex = 0;

        switch (spawnList[spawnIndex].type)
        {
            case "Scout":
                enemyIndex = 0;
                break;
            case "Bomber":
                enemyIndex = 1;
                break;
            case "TorpedoShip":
                enemyIndex = 2;
                break;
            case "Frigate":
                enemyIndex = 3;
                break;
            case "BattleCrusier":
                enemyIndex = 4;
                break;
            case "Dreadnought":
                enemyIndex = 5;
                break;
            case "End":
                enemyIndex = 6;
                break;
        }


        // enemyPoint�� spawnList���� ����
        int enemyPoint = spawnList[spawnIndex].point;

        // �� �������� Ȱ��ȭ
        GameObject enemy = GameManager.instance.objectManager.MakeObj(enemyIndex.ToString());

        // ���� ������ ���� ������ ��ġ��Ŵ
        enemy.transform.position = spawnPoints[enemyPoint].position;

        // ���� Rigidbody2D ������Ʈ�� Enemy ��ũ��Ʈ ������Ʈ�� ������
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player;

        // ���� ������ ���� ���� �ӵ��� ȸ���� ����
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

        // ���� ���� �׸����� �̵�
        spawnIndex++;

        // ��� ���� �����ߴ��� Ȯ���ϰ� ���̸� spawnEnd ������ ����
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // ���� �� ������ ���� nextSpawnDelay�� ������Ʈ
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }

}