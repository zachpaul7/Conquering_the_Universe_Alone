using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHP = 999;

    SpriteRenderer spriter;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public bool isHurt = false;

    void Awake()
    {
        spriter = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            isHurt = true;
            HitDamage(10);
        }
    }

    void HitDamage(int dmg)
    {
        playerHP -= dmg;

        if (playerHP > 0)
        {
            StartCoroutine("HitEffect");
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator HitEffect()
    {
        spriter.color = halfA;
        yield return new WaitForSeconds(0.1f);
        spriter.color = fullA;
        isHurt = false;
    }
}
