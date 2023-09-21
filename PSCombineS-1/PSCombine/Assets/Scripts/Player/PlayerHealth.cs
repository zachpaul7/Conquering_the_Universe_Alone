using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHP;
    public int maxHP;

    SpriteRenderer spriter;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public bool isHurt = false;

    void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
        maxHP = GameManager.instance.playerMaxHealth;
        playerHP = maxHP;
    }
    private void Update()
    {
        if (maxHP < GameManager.instance.playerMaxHealth)
        {
            maxHP = GameManager.instance.playerMaxHealth;
            playerHP++;
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            isHurt = true;
            HitDamage(1);
            gameObject.SetActive(false);
            GameManager.instance.StartCoroutine("RespawnPlayer");
        }
    }

    void HitDamage(int dmg)
    {
        playerHP -= dmg;

/*        if (playerHP > 0)
        {
            
            
        }
        else
        {
            
        }*/
    }


    IEnumerator HitEffect()
    {
        spriter.color = halfA;
        yield return new WaitForSeconds(0.1f);
        spriter.color = fullA;
        isHurt = false;
    }
}
