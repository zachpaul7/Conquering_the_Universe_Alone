using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{
    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
