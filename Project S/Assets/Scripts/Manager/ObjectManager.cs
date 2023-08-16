using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject bullet_Player_Cannon_Prefab;
    GameObject[] bullet_Player_Cannon;

    [SerializeField] private GameObject bullet_Player_Rocket_Prefab;
    GameObject[] bullet_Player_Rocket;

    [SerializeField] private GameObject bullet_Player_BFGun_Prefab;
    GameObject[] bullet_Player_BFGun;

    GameObject[] targetPool;

    private void Awake()
    {
        bullet_Player_Cannon = new GameObject[30];
        bullet_Player_Rocket = new GameObject[6];
        bullet_Player_BFGun = new GameObject[2];

        Generater();
    }

    private void Generater()
    {
        for(int index = 0; index < bullet_Player_Cannon.Length; index++)
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
    }

    public GameObject MakeObj(string type)
    {
        
        switch (type)
        {
            case "Bullet_Player_Cannon":
                targetPool = bullet_Player_Cannon;
                break;

            case "Bullet_Player_Rocket":
                targetPool = bullet_Player_Rocket;
                break;

            case "Bullet_Player_BFGun":
                targetPool = bullet_Player_BFGun;
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
