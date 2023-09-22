using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;

    [SerializeField] private float maxShotDelay;
    [SerializeField] private float curShotDelay;

    [SerializeField] private bool isBoss = false;
    [SerializeField] private int patternIndex;
    [SerializeField] private int curPatternCount;
    [SerializeField] private int[] maxPatternCount;

    [HideInInspector] public string eName;
    [HideInInspector] public float hp;
    [HideInInspector] public float speed;
    [HideInInspector] public int spawnType;

    private Color halfA = new Color32(255, 255, 255, 160);
    private Color fullA = new Color32(255, 255, 255, 255);

    private string fA;

    SpriteRenderer spriter;
    Rigidbody2D rgb2d;
    Collider2D coll;
    Animator animator;

    public GameObject player;
    [SerializeField] private float fireRate = 1f; // 발사 속도 

    void Awake()
    {
        FireCoroutine();
        coll = GetComponent<Collider2D>();
        rgb2d = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        eName = data.eName;
        hp = data.hp;
        speed = data.speed;
        spawnType = data.spawnType;
    }

    void Start()
    {

        rgb2d.velocity = Vector2.down * speed;

        switch (eName) 
        {
            case "0":

            case "1":
                rgb2d.velocity = Vector2.down * speed;
                
                break;

            case "2":
                StartCoroutine("TorpedoShipMove");
                break;

            case "3":
                StartCoroutine("FrigateMove");
                break;

            case "4":
                StartCoroutine("ElitetMove");
                break;

            case "5":
                StartCoroutine("BossMove");
                break;

            case "6":
                rgb2d.velocity = Vector2.down * speed;
                break;
        }
    }

    IEnumerator TorpedoShipMove()
    {
        rgb2d.velocity = Vector2.down * speed;
        yield return YieldCache.WaitForSeconds(1.5f);

        rgb2d.velocity = Vector2.zero;

        for (int i = 0; i < 4; i++)
        {
            animator.SetTrigger("Fire");
            yield return YieldCache.WaitForSeconds(1.35f);

            animator.SetBool("Reload", true);
            yield return YieldCache.WaitForSeconds(1f);

            if (i != 3)
            {
                animator.SetBool("Reload", false);
                yield return YieldCache.WaitForSeconds(1f);
            }
        }

        rgb2d.velocity = Vector2.up * speed;
    }

    IEnumerator FrigateMove()
    {
        if (spawnType == 0)
        {
            rgb2d.velocity = Vector2.down * speed;
            yield return YieldCache.WaitForSeconds(2.8f);

            rgb2d.velocity = Vector2.zero;
            yield return YieldCache.WaitForSeconds(1f);

            fA = "Bullet_Enemy_Special";
            FireAround();
            Debug.Log(maxPatternCount[patternIndex]);


            yield return YieldCache.WaitForSeconds(5f);

            rgb2d.velocity = Vector2.up * (speed * 0.75f);
            yield return YieldCache.WaitForSeconds(4f);
        }

    }

    IEnumerator ElitetMove()
    {
        yield return null;
    }

    IEnumerator BossMove()
    {
        yield return null;
    }

    IEnumerator HitEffect()
    {
        spriter.color = halfA;
        yield return YieldCache.WaitForSeconds(0.1f);
        spriter.color = fullA;
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;

            case 1:
                FireShot();
                break;

            case 2:
                FireArc();
                break;

            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        Debug.Log("전방 발싸");

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 0.7f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireShot()
    {
        Debug.Log("히히 발싸");

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 0.7f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireArc()
    {
        Debug.Log("부채 발싸");

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.7f);
        }
        else
        {
            Invoke("Think", 3);
        }
    }

    void FireAround()
    {
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = GameManager.instance.objectManager.MakeObj(fA);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireAround", 0.7f);
        }
        else
        {
            if (isBoss)
            {
                Invoke("Think", 3);
            }
        }

    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp > 0)
        {
            StartCoroutine("HitEffect");
        }
        if (hp <= 0)
        { 

            gameObject.SetActive(false);
            if (eName == "5")
            {
                GameManager.instance.ActControl();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BorderEnemy"))
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;

            if (eName == "End")
            {
                GameManager.instance.spawnManager.StageEnd();
            }
            
        }
    }

    private void Update()
    {
        // 타이머 업데이트
        curShotDelay += Time.deltaTime;

        // 총알 발사
        if (curShotDelay >= fireRate)
        {
            Fire();
            curShotDelay = 0f; // 타이머 초기화
        }
    }
    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire(); // 발사 메서드 호출로 총알을 생성하고 발사합니다.
            yield return new WaitForSeconds(fireRate); // 다음 발사까지 대기합니다.
        }

    }
    void Fire()
    {
        if (curShotDelay < maxShotDelay)
            return;

        {
            if (eName == "1")
            {
                GameObject bulletR = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Normal");
                GameObject bulletL = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Normal");

                bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                bulletL.transform.position = transform.position + Vector3.left * 0.2f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            }
            if (eName == "3")
            {
                GameObject bulletR = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Normal");
                GameObject bulletL = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Normal");

                bulletR.transform.position = transform.position + Vector3.right * 0.45f;
                bulletL.transform.position = transform.position + Vector3.left * 0.45f;
                
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.45f);
                Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.45f);

                rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
                rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
            }
            if (eName == "4")
            {
                GameObject bulletC = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Three");

                bulletC.transform.position = transform.position + Vector3.down;
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();

                Vector3 dirVecC = player.transform.position - (transform.position + Vector3.down);
                bulletC.transform.up = -dirVecC.normalized;

                // 총알을 발사합니다.
                rigidC.AddForce(dirVecC.normalized * 4, ForceMode2D.Impulse);
            }


        }
    }
}