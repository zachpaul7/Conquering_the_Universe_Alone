using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AllStats EnemyData;

    public int hp;
    public int dmg;
    public float moveSpeed;
    public string Name;
    public GameObject player;

    SpriteRenderer spriter;
    Rigidbody2D rgb2d;
    Collider2D coll;
    Animator animator;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
        rgb2d = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        moveSpeed = EnemyData.speed;
        hp = EnemyData.health;
        Name = EnemyData.objectName;

        rgb2d.velocity = Vector2.down * moveSpeed;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp > 0)
        {
            StartCoroutine("HitEffect");
        }
        else
        {
            gameObject.SetActive(false);
            if (Name == "Dreadnought")
            {
                GameManager.instance.actNum++;

                // 첫 번째로 비활성화되면 Act 2로, 두 번째로 비활성화되면 Act 3으로 넘어감
                switch (GameManager.instance.actNum)
                {
                    case 2:
                        GameManager.instance.stageManager.GoNextStoryAct("Act 2");
                        break;
                    case 3:
                        GameManager.instance.stageManager.GoNextStoryAct("Act 3");
                        break;
                    default:
                        // 필요한 경우 다른 처리 로직 추가 가능
                        break;
                }
            }
        }
    }

    IEnumerator HitEffect()
    {
        spriter.color = new Color32(255, 255, 255, 160);
        yield return new WaitForSeconds(0.1f);
        spriter.color = new Color32(255, 255, 255, 255);
        yield return null;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
            if (Name == "End")
            {
                GameManager.instance.spawnManager.StageEnd();
            }
        }
    }
}
