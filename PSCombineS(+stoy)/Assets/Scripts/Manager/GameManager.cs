using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameObject gameManager;
    public static GameManager instance = null;

    public GameObject player;
    public PlayerMove playerMove;
    public StageManager stageManager;
    public UpgradeController upgradeController;
    public SpawnManager spawnManager;
    public ObjectManager objectManager;

    [HideInInspector]public int playerMaxHealth;
    public int actNum;
    public bool isWork;

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

        if(SceneManager.GetActiveScene().buildIndex==2 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            player = GameObject.Find("Main Ship");
            spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
            playerMove = player.GetComponent<PlayerMove>();
            upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
            objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
            stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            player = null;
            spawnManager = null;
            playerMove = null;
            upgradeController = null;
            objectManager = null;
            stageManager = null;
        }

        actNum = 1; // actNum �ʱ�ȭ
        isWork = false;

        player = GameObject.Find("Main Ship");
        spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
        playerMove = player.GetComponent<PlayerMove>();
        upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
        objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
        stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();
    }
    
    private void Update()
    {
        CheckCompont();

        //Debug.Log(playerMaxHealth);

    }

    void CheckCompont()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            if (player == null)
            {
                player = GameObject.Find("Main Ship");
            }
            if (spawnManager == null)
            {
                spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
            }
            if (playerMove == null)
            {
                playerMove = player.GetComponent<PlayerMove>();
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
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 5)
        {
            player = null;
            spawnManager = null;
            playerMove = null;
            upgradeController = null;
            objectManager = null;
            stageManager = null;
        }


    }



    public void ActControl()
    {
        actNum++;

        // ù ��°�� ��Ȱ��ȭ�Ǹ� Act 2��, �� ��°�� ��Ȱ��ȭ�Ǹ� Act 3���� �Ѿ
        switch (actNum)
        {
            case 2:
                StartCoroutine(SceneChange());
                //stageManager.GoNextStoryAct("Act 2");
                break;
            case 3:
                stageManager.GoNextStoryAct("Story_Act 2");
                break;
            default:
                // �ʿ��� ��� �ٸ� ó�� ���� �߰� ����
                break;
        }
    }

    private IEnumerator SceneChange()
    {
        yield return YieldCache.WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public IEnumerator RespawnPlayer()
    {
        yield return YieldCache.WaitForSeconds(2f);

        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true);

        isWork = true;
    }
}
