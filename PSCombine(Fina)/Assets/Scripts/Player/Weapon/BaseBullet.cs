using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    public int dmg;

    public void Update()
    {
        dmg = GameManager.instance.damageManager.baseDmg;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "BorderBullet")
        {
            gameObject.SetActive(false);
        }

        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                /*Debug.Log(dmg);*/
                enemy.TakeDamage(dmg);
                Debug.Log(dmg);
                gameObject.SetActive(false);
            }
        }
    }
}
