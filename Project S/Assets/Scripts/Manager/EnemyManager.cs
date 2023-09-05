using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
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

    GameObject[] targetPool;

    private void Awake()
    {
        enemy_Scout = new GameObject[15];
        enemy_Bomber = new GameObject[15];
        enemy_TorpedoShip = new GameObject[5];
        enemy_Frigate = new GameObject[5];
        enemy_BattleCrusier = new GameObject[3];
        enemy_Dreadnought = new GameObject[1];
        enemy_End = new GameObject[1];

        Generater();
    }

    private void Generater()
    {
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
    }

    public GameObject MakeObj(int type)
    {

        switch (type)
        {
            case 0:
                targetPool = enemy_Scout;
                break;

            case 1:
                targetPool = enemy_Bomber;
                break;

            case 2:
                targetPool = enemy_TorpedoShip;
                break;

            case 3:
                targetPool = enemy_Frigate;
                break;

            case 4:
                targetPool = enemy_BattleCrusier;
                break;

            case 5:
                targetPool = enemy_Dreadnought;
                break;
            case 6:
                targetPool = enemy_End;
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
