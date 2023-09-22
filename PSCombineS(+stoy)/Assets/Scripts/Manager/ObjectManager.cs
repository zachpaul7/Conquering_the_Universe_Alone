using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //���� ������ �� ������Ʈ
    [SerializeField] private GameObject bullet_Player_Cannon_Prefab;
    GameObject[] bullet_Player_Cannon;

    [SerializeField] private GameObject bullet_Player_Rocket_Prefab;
    GameObject[] bullet_Player_Rocket;

    [SerializeField] private GameObject bullet_Player_BFGun_Prefab;
    GameObject[] bullet_Player_BFGun;


    //�� ������ �� ������Ʈ
    [SerializeField] private GameObject enemy_Scout_Prefab;
    GameObject[] enemy_Scout;

    [SerializeField] private GameObject enemy_Bomber_Prefab;
    GameObject[] enemy_Bomber;

    [SerializeField] private GameObject enemy_TorpedoShip_Prefab;
    GameObject[] enemy_TorpedoShip;

    [SerializeField] private GameObject enemy_Frigate_Prefab;
    GameObject[] enemy_Frigate;

    [SerializeField] private GameObject enemy_BattleCrusier_Prefab;
    GameObject[] enemy_BattleCrusier;

    [SerializeField] private GameObject enemy_Dreadnought_Prefab;
    GameObject[] enemy_Dreadnought;

    [SerializeField] private GameObject enemy_End_Prefab;
    GameObject[] enemy_End;


    //�� ���� ������ �� ������Ʈ
    [SerializeField] private GameObject bullet_Enemy_Normal_Prefab;
    GameObject[] bullet_Enemy_Normal;

    [SerializeField] private GameObject bullet_Enemy_Special_Prefab;
    GameObject[] bullet_Enemy_Special;

    [SerializeField] private GameObject bullet_Enemy_Rocket_Prefab;
    GameObject[] bullet_Enemy_Rocket;

    [SerializeField] private GameObject bullet_Enemy_Three_Prefab;
    GameObject[] bullet_Enemy_Three;

    GameObject[] targetPool;

    private void Awake()
    {
        //�Ѿ� ������ ���� 
        bullet_Player_Cannon = new GameObject[30];
        bullet_Player_Rocket = new GameObject[6];
        bullet_Player_BFGun = new GameObject[2];

        //�� ������ ���� 
        enemy_Scout = new GameObject[15];
        enemy_Bomber = new GameObject[15];
        enemy_TorpedoShip = new GameObject[5];
        enemy_Frigate = new GameObject[5];
        enemy_BattleCrusier = new GameObject[3];
        enemy_Dreadnought = new GameObject[1];
        enemy_End = new GameObject[1];

        //�� �Ѿ� ������ ���� 
        bullet_Enemy_Normal = new GameObject[300];
        bullet_Enemy_Special = new GameObject[300];
        bullet_Enemy_Rocket = new GameObject[60];
        bullet_Enemy_Three = new GameObject[60];

        Generater();
    }

    private void Generater()
    {
        //�Ѿ� ���� �� ��Ȱ��ȭ
        for (int index = 0; index < bullet_Player_Cannon.Length; index++)
        {
            bullet_Player_Cannon[index] = Instantiate(bullet_Player_Cannon_Prefab, transform);
            bullet_Player_Cannon[index].SetActive(false);
        }

        for (int index = 0; index < bullet_Player_Rocket.Length; index++)
        {
            bullet_Player_Rocket[index] = Instantiate(bullet_Player_Rocket_Prefab, transform);
            bullet_Player_Rocket[index].SetActive(false);
        }

        for (int index = 0; index < bullet_Player_BFGun.Length; index++)
        {
            bullet_Player_BFGun[index] = Instantiate(bullet_Player_BFGun_Prefab, transform);
            bullet_Player_BFGun[index].SetActive(false);
        }

        //�� ���� �� ��Ȱ��ȭ
        for (int index = 0; index < enemy_Scout.Length; index++)
        {
            enemy_Scout[index] = Instantiate(enemy_Scout_Prefab, transform);
            enemy_Scout[index].SetActive(false);
        }

        for (int index = 0; index < enemy_Bomber.Length; index++)
        {
            enemy_Bomber[index] = Instantiate(enemy_Bomber_Prefab, transform);
            enemy_Bomber[index].SetActive(false);
        }

        for (int index = 0; index < enemy_TorpedoShip.Length; index++)
        {
            enemy_TorpedoShip[index] = Instantiate(enemy_TorpedoShip_Prefab, transform);
            enemy_TorpedoShip[index].SetActive(false);
        }

        for (int index = 0; index < enemy_Frigate.Length; index++)
        {
            enemy_Frigate[index] = Instantiate(enemy_Frigate_Prefab, transform);
            enemy_Frigate[index].SetActive(false);
        }

        for (int index = 0; index < enemy_BattleCrusier.Length; index++)
        {
            enemy_BattleCrusier[index] = Instantiate(enemy_BattleCrusier_Prefab, transform);
            enemy_BattleCrusier[index].SetActive(false);
        }

        for (int index = 0; index < enemy_Dreadnought.Length; index++)
        {
            enemy_Dreadnought[index] = Instantiate(enemy_Dreadnought_Prefab, transform);
            enemy_Dreadnought[index].SetActive(false);
        }

        for (int index = 0; index < enemy_End.Length; index++)
        {
            enemy_End[index] = Instantiate(enemy_End_Prefab, transform);
            enemy_End[index].SetActive(false);
        }


        //�� �Ѿ� ���� �� ��Ȱ��ȭ
        for (int index = 0; index < bullet_Enemy_Normal.Length; index++)
        {
            bullet_Enemy_Normal[index] = Instantiate(bullet_Enemy_Normal_Prefab, transform);
            bullet_Enemy_Normal[index].SetActive(false);
        }

        for (int index = 0; index < bullet_Enemy_Special.Length; index++)
        {
            bullet_Enemy_Special[index] = Instantiate(bullet_Enemy_Special_Prefab, transform);
            bullet_Enemy_Special[index].SetActive(false);
        }

        for (int index = 0; index < bullet_Enemy_Rocket.Length; index++)
        {
            bullet_Enemy_Rocket[index] = Instantiate(bullet_Enemy_Rocket_Prefab, transform);
            bullet_Enemy_Rocket[index].SetActive(false);
        }
        for (int index = 0; index < bullet_Enemy_Three.Length; index++)
        {
            bullet_Enemy_Three[index] = Instantiate(bullet_Enemy_Three_Prefab, transform);
            bullet_Enemy_Three[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {

        switch (type)
        {
            //�Ѿ� ��ȯ
            case "Bullet_Player_Cannon":
                targetPool = bullet_Player_Cannon;
                break;

            case "Bullet_Player_Rocket":
                targetPool = bullet_Player_Rocket;
                break;

            case "Bullet_Player_BFGun":
                targetPool = bullet_Player_BFGun;
                break;


            //�� ��ȯ
            case "0":
                targetPool = enemy_Scout;
                break;

            case "1":
                targetPool = enemy_Bomber;
                break;

            case "2":
                targetPool = enemy_TorpedoShip;
                break;

            case "3":
                targetPool = enemy_Frigate;
                break;

            case "4":
                targetPool = enemy_BattleCrusier;
                break;

            case "5":
                targetPool = enemy_Dreadnought;
                break;

            case "6":
                targetPool = enemy_End;
                break;

            //�Ѿ� ��ȯ
            case "Bullet_Enemy_Normal":
                targetPool = bullet_Enemy_Normal;
                break;

            case "Bullet_Enemy_Special":
                targetPool = bullet_Enemy_Special;
                break;

            case "Bullet_Enemy_Rocket":
                targetPool = bullet_Enemy_Rocket;
                break;
            case "Bullet_Enemy_Three":
                targetPool = bullet_Enemy_Three;
                break;

        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }
}
