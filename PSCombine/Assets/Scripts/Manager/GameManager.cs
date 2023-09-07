using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PlayerMove playerMove;
    public StageManager stageManager;
    public UpgradeController upgradeController;
    public SpawnManager spawnManager;
    public ObjectManager objectManager;

    public int actNum;
    public int playerHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        actNum = 1; // actNum �ʱ�ȭ

        spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
        playerMove = GameObject.Find("Main Ship").GetComponent<PlayerMove>();
        upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
        objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
        stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();
    }

    private void Update()
    {
        CheckCompont();

    }

    void CheckCompont()
    {
        if (spawnManager == null)
        {
            spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
        }
        if (playerMove == null)
        {
            playerMove = GameObject.Find("Main Ship").GetComponent<PlayerMove>();
        }
        if (upgradeController == null)
        {
            upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
        }
        if (objectManager == null)
        {
            objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
        }
        if (stageManager == null)
        {
            stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();
        }
    }

    public void ActControl()
    {
        actNum++;

        // ù ��°�� ��Ȱ��ȭ�Ǹ� Act 2��, �� ��°�� ��Ȱ��ȭ�Ǹ� Act 3���� �Ѿ
        switch (actNum)
        {
            case 2:
                stageManager.GoNextStoryAct("Act 2");
                break;
            case 3:
                stageManager.GoNextStoryAct("Act 3");
                break;
            default:
                // �ʿ��� ��� �ٸ� ó�� ���� �߰� ����
                break;
        }
    }
}
