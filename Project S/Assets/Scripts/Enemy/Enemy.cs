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
}
