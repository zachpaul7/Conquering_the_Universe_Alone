using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    Rigidbody2D rgb2d;
    SpriteRenderer sprite;

    void Awake()
    {
        rgb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        rgb2d.velocity = Vector2.down * moveSpeed;

        sprite.flipY = true;
    }
}
