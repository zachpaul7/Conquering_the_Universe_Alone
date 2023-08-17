using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 999;
    public int dmg = 1;
    public float moveSpeed = 1f;
    public string eName;
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
            Destroy(gameObject);
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
            Destroy(gameObject);
            transform.rotation = Quaternion.identity;
            if (eName == "L")
            {
                GameManager.instance.stageManager.StageEnd();
            }
        }
    }
}
