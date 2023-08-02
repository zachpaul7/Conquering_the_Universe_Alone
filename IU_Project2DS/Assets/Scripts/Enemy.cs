using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AllStats stats;
    public GameObject player;
    public GameObject bulletObjA;
    public GameManager gameManager;

    public float maxShotDelay;
    public float curShotDelay;

    private Bullet bullet;
    public float speed;
    public int health;
    public string eName;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    void Awake()
    {
        bullet = FindObjectOfType<Bullet>();
        speed = stats.speed;
        health = stats.health;
        eName = stats.objectName;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;

        Debug.Log(rigid.velocity.y);
    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    {
        if (curShotDelay < maxShotDelay) // 총알을 연속으로 발사하지 못하게 하기 위한 제한
            return;

        if (eName == "S")
        {
            GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
            bullet.transform.position = transform.position;
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector3 dirVec = player.transform.position - transform.position;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
        else if (eName == "L")
        {
            GameObject bulletR = Instantiate(bulletObjA, transform.position, transform.rotation);
            bulletR.transform.position = transform.position + Vector3.right * 0.3f;
            GameObject bulletL = Instantiate(bulletObjA, transform.position, transform.rotation);
            bulletL.transform.position = transform.position + Vector3.left * 0.3f;

            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            rigidR.AddForce(dirVecR.normalized * 4, ForceMode2D.Impulse);
            rigidL.AddForce(dirVecL.normalized * 4, ForceMode2D.Impulse);
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    public void OnHit(int bulletDmg)
    {
        health -= bulletDmg;
        if (health <= 0)
        {
            Destroy(gameObject);

            if (eName == "L")
            {
                gameManager.StageEnd();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border Bullet")
        {
            Destroy(gameObject);
            transform.rotation = Quaternion.identity;
        }
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.bulletDmg);
            Destroy(collision.gameObject);
        }

    }
}
