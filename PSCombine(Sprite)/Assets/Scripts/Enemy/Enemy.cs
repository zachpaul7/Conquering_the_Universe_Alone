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

    [SerializeField] private bool isDead = false;
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

    private Transform playerTransform;
    SpriteRenderer spriter;
    Rigidbody2D rgb2d;
    Collider2D coll;
    Animator animator;

    public GameObject player;
    [SerializeField] private float fireRate = 1f; // �߻� �ӵ� 

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
        yield return YieldCache.WaitForSeconds(3f);

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

    public void TorpedoFire()
    {
        switch (GameManager.instance.actNum)
        {
            case 1:
                StartCoroutine(TorpedoFireCoroutineA1());
                break;
            case 2:
                StartCoroutine(TorpedoFireCoroutineA2());
                break;
            case 3:
                StartCoroutine(TorpedoFireCoroutineA3());
                break;
        }
    }

    IEnumerator TorpedoFireCoroutineA1()
    {
        yield return YieldCache.WaitForSeconds(0.2f);

        for (int i = 0; i < 6; i++)
        {
            yield return YieldCache.WaitForSeconds(0.17f);

            Vector3 rocketPosition = transform.position;

            // ���� ��ġ
            switch (i)
            {
                case 0:
                    rocketPosition += Vector3.left * 0.177f + Vector3.down * 0.2f;
                    break;
                case 1:
                    rocketPosition += Vector3.right * 0.177f + Vector3.down * 0.2f;
                    break;
                case 2:
                    rocketPosition += Vector3.left * 0.3f + Vector3.down * 0.2f;
                    break;
                case 3:
                    rocketPosition += Vector3.right * 0.3f + Vector3.down * 0.2f;
                    break;
                case 4:
                    rocketPosition += Vector3.left * 0.427f + Vector3.down * 0.2f;
                    break;
                case 5:
                    rocketPosition += Vector3.right * 0.427f + Vector3.down * 0.2f;
                    break;
            }

            // ���� ����
            GameObject t = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Rocket");
            t.transform.position = rocketPosition;

            // �� ������ rigidbody ��������
            Rigidbody2D rigidt = t.GetComponent<Rigidbody2D>();

            // �߻�
            rigidt.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
        }
    }

    IEnumerator TorpedoFireCoroutineA2()
    {
        yield return YieldCache.WaitForSeconds(0.2f);

        for (int i = 0; i < 3; i++)
        {
            yield return YieldCache.WaitForSeconds(0.17f);

            Vector3 rocketPosition1 = transform.position;
            Vector3 rocketPosition2 = transform.position;

            // ���� ��ġ
            switch (i)
            {
                case 0:
                    rocketPosition1 += Vector3.left * 0.177f + Vector3.down * 0.1f;
                    rocketPosition2 += Vector3.right * 0.177f + Vector3.down * 0.1f;
                    break;

                case 1:
                    rocketPosition1 += Vector3.left * 0.3f + Vector3.down * 0.1f;
                    rocketPosition2 += Vector3.right * 0.3f + Vector3.down * 0.1f;
                    break;

                case 2:
                    rocketPosition1 += Vector3.left * 0.427f + Vector3.down * 0.1f;
                    rocketPosition2 += Vector3.right * 0.427f + Vector3.down * 0.1f;
                    break;
            }

            // ���� ����
            GameObject t1 = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Rocket");
            t1.transform.position = rocketPosition1;
            GameObject t2 = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Rocket");
            t2.transform.position = rocketPosition2;

            // �� ������ rigidbody ��������
            Rigidbody2D rigidt1 = t1.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidt2 = t2.GetComponent<Rigidbody2D>();
            // �߻�
            rigidt1.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            rigidt2.AddForce(Vector2.down * 5, ForceMode2D.Impulse);

            yield return YieldCache.WaitForSeconds(0.17f);
        }
    }

    IEnumerator TorpedoFireCoroutineA3()
    {
        yield return YieldCache.WaitForSeconds(0.2f);

        for (int i = 0; i < 3; i++)
        {
            yield return YieldCache.WaitForSeconds(0.17f);

            Vector3 rocketPosition1 = transform.position;
            Vector3 rocketPosition2 = transform.position;

            // ���� ��ġ
            switch (i)
            {
                case 0:
                    rocketPosition1 += Vector3.left * 0.057f + Vector3.down * 0.1f;
                    rocketPosition2 += Vector3.right * 0.057f + Vector3.down * 0.1f;
                    break;

                case 1:
                    rocketPosition1 += Vector3.left * 0.161f + Vector3.down * 0.1f;
                    rocketPosition2 += Vector3.right * 0.161f + Vector3.down * 0.1f;
                    break;

                case 2:
                    rocketPosition1 += Vector3.left * 0.269f + Vector3.down * 0.1f;
                    rocketPosition2 += Vector3.right * 0.269f + Vector3.down * 0.1f;
                    break;
            }

            // ���� ����
            GameObject t1 = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Rocket");
            t1.transform.position = rocketPosition1;
            GameObject t2 = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Rocket");
            t2.transform.position = rocketPosition2;

            // �� ������ rigidbody ��������
            Rigidbody2D rigidt1 = t1.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidt2 = t2.GetComponent<Rigidbody2D>();
            // �߻�
            rigidt1.AddForce(Vector2.down * 5, ForceMode2D.Impulse);
            rigidt2.AddForce(Vector2.down * 5, ForceMode2D.Impulse);

            yield return YieldCache.WaitForSeconds(0.17f);
        }
    }

    IEnumerator FrigateMove()
    {
        if (spawnType == 0)
        {
            rgb2d.velocity = Vector2.down * speed;
            yield return YieldCache.WaitForSeconds(2.8f);

            rgb2d.velocity = Vector2.zero;
            yield return YieldCache.WaitForSeconds(1f);

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
        yield return YieldCache.WaitForSeconds(3.5f);

        rgb2d.velocity = Vector2.zero;
        Invoke("Think", 2);
    }

    private void Update()
    {
        playerTransform = player.transform;
        // Ÿ�̸� ������Ʈ
        curShotDelay += Time.deltaTime;

        // �Ѿ� �߻�
        if (curShotDelay >= fireRate)
        {
            Fire();
            curShotDelay = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    public void TakeDamage(int dmg)
    {
        if (isDead)
            return;

        hp -= dmg;

        if (hp > 0)
        {
            StartCoroutine("HitEffect");
        }
        else if (hp <= 0)
        {
            isDead = true;
            rgb2d.velocity = Vector3.zero;
            coll.enabled = false;

            animator.SetTrigger("Dead");
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

                bulletR.transform.up = -dirVecR.normalized;
                bulletL.transform.up = -dirVecL.normalized;

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

                // �Ѿ��� �߻��մϴ�.
                rigidC.AddForce(dirVecC.normalized * 4, ForceMode2D.Impulse);
            }


        }
    }

    void FireForward()
    {
        if (hp <= 0)
            return;
        GameObject bulletR = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Three");
        GameObject bulletL = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Three");

        bulletR.transform.position = transform.position + Vector3.right * 0.45f;
        bulletL.transform.position = transform.position + Vector3.left * 0.45f;

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

        Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.45f);
        Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.45f);

        bulletR.transform.up = -dirVecR.normalized;
        bulletL.transform.up = -dirVecL.normalized;

        rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
        rigidL.AddForce(dirVecL.normalized * 5, ForceMode2D.Impulse);
        Debug.Log("FireForward");

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireForward", 0.7f);
        }
        else
        {
            if (isBoss)
            {
                Invoke("Think", 3);
            }
        }
    }

    void FireShot()
    {
        if (hp <= 0)
            return;

        Vector2 dirVec = playerTransform.position - transform.position; // �ʱ�ȭ �߰�

        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Special");
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 ranVec = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        }
        Debug.Log("FireShot");

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireShot", 0.7f);
        }
        else
        {
            if (isBoss)
            {
                Invoke("Think", 3);
            }
        }
    }


    void FireArc()
    {
        if (hp <= 0)
            return;

        GameObject bullet = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Special");

        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 8 * curPatternCount/maxPatternCount[patternIndex]),-1);

        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
        Debug.Log("FireArc");

        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FireArc", 0.15f);
        }
        else
        {
            if (isBoss)
            {
                Invoke("Think", 3);
            }
        }
    }

    void FireAround()
    {
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {
            GameObject bullet = GameManager.instance.objectManager.MakeObj("Bullet_Enemy_Normal");
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

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire(); // �߻� �޼��� ȣ��� �Ѿ��� �����ϰ� �߻��մϴ�.
            yield return new WaitForSeconds(fireRate); // ���� �߻���� ����մϴ�.
        }

    }

    IEnumerator HitEffect()
    {
        spriter.color = halfA;
        yield return YieldCache.WaitForSeconds(0.1f);
        spriter.color = fullA;
    }

    public void DestroyEndAnimation()
    {
        gameObject.SetActive(false);

        if (eName == "5")
        {
            GameManager.instance.playerHealth.isStage = true;
            GameManager.instance.ActControl();
            
        }
    }
}