using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{

    public GameObject player;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public List<Spawn> spawnList;
    public Transform[] spawnPoints;

    public int spawnIndex;
    public bool spawnEnd;

    public Animator stageAnim;
    public Animator fadeAnim;

    public UpgradePanelManager upgradePanelManager;

    void Start()
    {
        // 스폰 리스트와 적 오브젝트 배열 초기화
        spawnList = new List<Spawn>();
        StageStart();
        upgradePanelManager = GameObject.Find("---UpgradeController---").GetComponent<UpgradePanelManager>();
    }

    void Update()
    {
        // 시간에 따라 현재 스폰 딜레이를 업데이트
        curSpawnDelay += Time.deltaTime;

        // 스폰 딜레이가 다 되었고 아직 스폰이 끝나지 않았으면 적을 스폰
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
        StartCoroutine("startAni");
        ReadSpawnFile();

    }
    public void StageEnd()
    {
        fadeAnim.SetTrigger("Out");
        StartCoroutine("playerPos");
        // 스테이지 증가
        GameManager.instance.stage++;

        if (GameManager.instance.stage > 4)
        {

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
        // 스폰 리스트를 비우고 스폰 인덱스와 스폰 종료 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        // 지정된 텍스트 파일로부터 스폰 데이터 읽어오기
        TextAsset textFile = Resources.Load("SpawnText/Act " + GameManager.instance.actNum + "/Stage " + GameManager.instance.stage) as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        // 텍스트 파일의 각 줄을 읽고 파싱하여 스폰 객체를 생성
        while (stringReader != null)
        {
            string line = stringReader.ReadLine();

            // 파일의 끝에 도달했는지 확인
            if (line == null)
                break;

            // 새로운 스폰 객체를 생성하고 속성을 채움
            Spawn spawnData = gameObject.AddComponent<Spawn>();

            spawnData.type = line.Split(',')[0];
            spawnData.point = int.Parse(line.Split(',')[1]);
            spawnData.delay = float.Parse(line.Split(',')[2]);

            spawnList.Add(spawnData);
        }

        // stringReader를 닫고 다음 스폰 딜레이를 스폰 리스트의 첫 번째 항목으로 설정
        stringReader.Close();
        nextSpawnDelay = spawnList[0].delay;
    }

    void SpawnEnemy()
    {
        // 스폰이 종료되었는지 확인
        if (spawnEnd)
        {
            return;
        }

        // 스폰 리스트에서 지정된 타입에 따라 enemyIndex 결정
        int enemyIndex = 0;

        switch (spawnList[spawnIndex].type)
        {
            case "0":
                enemyIndex = 0;
                break;
            case "1":
                enemyIndex = 1;
                break;
            case "2":
                enemyIndex = 2;
                break;
            case "3":
                enemyIndex = 3;
                break;
            case "4":
                enemyIndex = 4;
                break;
            case "5":
                enemyIndex = 5;
                break;
            case "End":
                enemyIndex = 6;
                break;
        }


        // enemyPoint를 spawnList에서 얻음
        int enemyPoint = spawnList[spawnIndex].point;

        // 적 프리팹을 활성화
        GameObject enemy = GameManager.instance.objectManager.MakeObj(enemyIndex.ToString());

        // 적을 지정된 스폰 지점에 위치시킴
        enemy.transform.position = spawnPoints[enemyPoint].position;

        // 적의 Rigidbody2D 컴포넌트와 Enemy 스크립트 컴포넌트를 가져옴
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        enemyLogic.player = player;

        // 스폰 지점에 따라 적의 속도와 회전을 설정
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

        // 다음 스폰 항목으로 이동
        spawnIndex++;

        // 모든 적을 스폰했는지 확인하고 참이면 spawnEnd 변수를 설정
        if (spawnIndex == spawnList.Count)
        {
            spawnEnd = true;
            return;
        }

        // 다음 적 스폰을 위해 nextSpawnDelay를 업데이트
        nextSpawnDelay = spawnList[spawnIndex].delay;
    }
    IEnumerator startAni() // 스테이지 애니메이션 코루틴
    {
        yield return YieldCache.WaitForSeconds(0.1f);
        stageAnim.GetComponent<Text>().text = "Stage " + GameManager.instance.stage + "\nStart";
        stageAnim.SetTrigger("On");
        ChangeBGM();
        fadeAnim.SetTrigger("In");


    }
    IEnumerator playerPos() // 스테이지 애니메이션 코루틴
    {
        yield return YieldCache.WaitForSeconds(3f);
        Vector3 respawnEndPosition = new Vector3(0, -4, 0);
        player.transform.position = respawnEndPosition;
    }

    public void ChangeBGM()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            GameManager.instance.soundManager.PlayBGM("Act1");
            Debug.Log("브금이 시작");
            if (GameManager.instance.stage == 2)
            {
                GameManager.instance.soundManager.StopBGM();
                Debug.Log("브금이 중단되었음");
                GameManager.instance.soundManager.PlayBGM("Boss");
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            GameManager.instance.soundManager.PlayBGM("Act2");
            Debug.Log("브금이 시작");
            if (GameManager.instance.stage == 2)
            {
                GameManager.instance.soundManager.StopBGM();
                Debug.Log("브금이 중단되었음");
                GameManager.instance.soundManager.PlayBGM("Boss");
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 6)
        {
            GameManager.instance.soundManager.PlayBGM("Act3");
            Debug.Log("브금이 시작");
            if (GameManager.instance.stage == 2)
            {
                GameManager.instance.soundManager.StopBGM();
                Debug.Log("브금이 중단되었음");
                GameManager.instance.soundManager.PlayBGM("Boss");
            }
        }

    }

}