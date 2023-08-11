using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public float nextSpawnDelay;
    public float curSpawnDelay;

    public List<Spawn> spawnList;
    public Transform[] spawnPoints;

    public int spawnIndex;
    public bool spawnEnd;

    private int stage;

    void Awake()
    {
        // 스폰 리스트와 적 오브젝트 배열 초기화
        spawnList = new List<Spawn>();
        StageStart();
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

    public void StageStart()
    {
        ReadSpawnFile();
    }

    public void StageEnd()
    {
        stage++;
    }

    void ReadSpawnFile()
    {
        // 스폰 리스트를 비우고 스폰 인덱스와 스폰 종료 변수 초기화
        spawnList.Clear();
        spawnIndex = 0;
        spawnEnd = false;
        
        // 지정된 텍스트 파일로부터 스폰 데이터 읽어오기
        TextAsset textFile = Resources.Load("SpawnText/Stage " + stage) as TextAsset;
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

            spawnData.actNumber = int.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);
            spawnData.delay = float.Parse(line.Split(',')[3]);

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
        }

        // enemyPoint를 spawnList에서 얻음
        int enemyPoint = spawnList[spawnIndex].point;
        int actNumber = spawnList[spawnIndex].actNumber;

        // enemyIndex에 해당하는 적 프리팹을 불러옴
        GameObject prefab = Resources.Load<GameObject>("Enemies/Act " + actNumber + "/Enemy " + enemyIndex);

        // 적 프리팹을 인스턴스화
        GameObject enemy = Instantiate(prefab);

        // 적을 지정된 스폰 지점에 위치시킴
        enemy.transform.position = spawnPoints[enemyPoint].position;

        // 적의 Rigidbody2D 컴포넌트와 Enemy 스크립트 컴포넌트를 가져옴
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();

        //enemyLogic.player = player;
        //enemyLogic.gameManager = this;
        //// 스폰 지점에 따라 적의 속도와 회전을 설정
        //if (enemyPoint == 5 || enemyPoint == 6)
        //{
        //    enemy.transform.Rotate(Vector3.forward * 45);
        //    rigid.velocity = new Vector2(enemyLogic.speed, -1);
        //}
        //else if (enemyPoint == 7 || enemyPoint == 8)
        //{
        //    enemy.transform.Rotate(Vector3.back * 45);
        //    rigid.velocity = new Vector2(enemyLogic.speed * (-1), -1);
        //}
        //else
        //{
        //    rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
        //}

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

}
