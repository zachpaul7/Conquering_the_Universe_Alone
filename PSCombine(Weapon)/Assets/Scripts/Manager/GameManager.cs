using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    GameObject gameManager;
    public static GameManager instance = null;

    public GameObject player;
    public PlayerMove playerMove;
    public StageManager stageManager;
    public UpgradeController upgradeController;
    public GameObject upgrade;
    public SpawnManager spawnManager;
    public ObjectManager objectManager;
    public UpgradePanelManager upm;
    public PlayerHealth playerHealth;

    [HideInInspector] public int playerMaxHealth;
    [HideInInspector] public int life;
    public int actNum;
    public bool isWork;
    public int playerPower;
    public int stage;

    public Transform playerPos;

    public Image[] lifeImages;

    public GameObject canvas;

    public bool isRocketUnlocked = false;

    private void Awake()
    {
        
        if (instance == null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            if (instance != this )
            {
                Destroy(this.gameObject);
            }
        }
        if(SceneManager.GetActiveScene().buildIndex==2 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            canvas = GameObject.Find("Canvas");
            canvas.SetActive(true);
            player = GameObject.Find("Main Ship");
            spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
            playerMove = player.GetComponent<PlayerMove>();
            upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
            objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
            stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            player = null;
            spawnManager = null;
            playerMove = null;
            upgradeController = null;
            objectManager = null;
            stageManager = null;
            playerHealth = null;
        }


        actNum = 1; // actNum 초기화
        playerPower = 1;
        isWork = false;
        stage = 0;
        player = GameObject.Find("Main Ship");
        spawnManager = GameObject.Find("---SpawnManager---").GetComponent<SpawnManager>();
        playerMove = player.GetComponent<PlayerMove>();
        upgradeController = GameObject.Find("---UpgradeController---").GetComponent<UpgradeController>();
        objectManager = GameObject.Find("---ObjectManager---").GetComponent<ObjectManager>();
        stageManager = GameObject.Find("---StageManager---").GetComponent<StageManager>();
        playerHealth = player.GetComponent<PlayerHealth>();
        
    }
    
    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 5; index++) {
            lifeImages[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < life; index++)
        {
            lifeImages[index].color = new Color(1, 1, 1, 1);
        }
    }

    private void Update()
    {

        CheckCompont();
        if (SceneManager.GetActiveScene().buildIndex == 2 || SceneManager.GetActiveScene().buildIndex == 4 || SceneManager.GetActiveScene().buildIndex == 6)
        {
            canvas.SetActive(true);
            life = GameManager.instance.playerHealth.playerHP; // life에 player 현재 체력 저장
            UpdateLifeIcon(life); // 체력 전투씬에서만 실시간 업데이트
        }
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            canvas.SetActive(false);
        }
        

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
            if (playerHealth == null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
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
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 5 || SceneManager.GetActiveScene().buildIndex == 7 || SceneManager.GetActiveScene().buildIndex == 8)
        {
            player = null;
            spawnManager = null;
            playerMove = null;
            upgradeController = null;
            objectManager = null;
            stageManager = null;
            playerHealth = null;
        }


    }

    public void ActControl()
    {
        Debug.Log("Act");
        actNum++;
        StartCoroutine(SceneChange());
    }

    public IEnumerator SceneChange()
    {
        yield return YieldCache.WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        isRocketUnlocked = true;

        stage = 0;
        GameManager.instance.playerHealth.isStage = false;

    }
    public IEnumerator GameOver()
    {
        yield return YieldCache.WaitForSeconds(4f);
        SceneManager.LoadScene("GameOver");
    }



    public IEnumerator RespawnPlayer() // 플레이어 생성
    {
        yield return YieldCache.WaitForSeconds(1f);
        if (life > 0)
        {

            player.transform.position = Vector3.down * 3.5f;
            player.SetActive(true);

            isWork = true;
            Vector3 respawnStartPosition = new Vector3(0,-6,0); // 시작 지점
            Vector3 respawnEndPosition = new Vector3(0,-4,0); // 리스폰 위치

            float duration = 2.0f; // 이동에 걸릴 시간

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                // Lerp를 사용하여 부드러운 이동을 수행합니다.
                player.transform.position = Vector3.Lerp(respawnStartPosition, respawnEndPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // 최종 위치로 설정
            player.transform.position = respawnEndPosition;
        }
       
    }
}
