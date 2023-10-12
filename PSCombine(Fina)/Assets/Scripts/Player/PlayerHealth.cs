using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public int playerHP;
    public int maxHP;

    SpriteRenderer spriter;

    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    public bool isHurt;
    public bool isStage;
    private void Awake()
    {
        isHurt = false;
        isStage = false;

    }
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
        if (isHurt == true)
        {
            StartCoroutine("HitEffect");
        }

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isStage && !isHurt && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet"))
        {
            if (playerHP > 0) 
            {
                isHurt = true;
                HitDamage(1);
                gameObject.SetActive(false);
                GameManager.instance.StartCoroutine("RespawnPlayer");
               
            }
            
            if (playerHP == 0)
            {
                gameObject.SetActive(false);          
                GameManager.instance.spawnManager.fadeAnim.SetTrigger("Out");
                GameManager.instance.StartCoroutine("GameOver");
            }


        }
    }

    void HitDamage(int dmg)
    {
        playerHP -= dmg;
    }

    IEnumerator HitEffect() // ¹«Àû ÀÌÆåÆ®
    {
        spriter.color = halfA;
        yield return YieldCache.WaitForSeconds(4f);
        spriter.color = fullA;
        isHurt = false;
    }

}
