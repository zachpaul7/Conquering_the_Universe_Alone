using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AllStats stats;
    public int bulletDmg;

    void Start()
    {
        bulletDmg = stats.bulletDmg;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border Bullet")
        {
            Destroy(gameObject);
        }
    }
}
