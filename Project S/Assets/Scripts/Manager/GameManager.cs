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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            actNum = 1; // actNum √ ±‚»≠
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }

        spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
        playerMove = GameObject.Find("Main Ship").GetComponent<PlayerMove>();
        upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
        objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
        stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();


    }

    private void Update()
    {
        if(spawnManager == null)
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
}
