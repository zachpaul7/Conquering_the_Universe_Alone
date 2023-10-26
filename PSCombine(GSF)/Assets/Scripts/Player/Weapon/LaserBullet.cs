using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserBullet : MonoBehaviour
{
    public int damage = 0; // 공격력

    private Enemy currentEnemy; // 현재 적을 추적하는 변수

    [SerializeField] float atkTime;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                currentEnemy = enemy; // 현재 적을 추적 변수에 저장
                InvokeRepeating("DamageEnemy", 0f, atkTime); // 0초 후부터 0.5초마다 DamageEnemy() 함수를 호출합니다.
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CancelInvoke("DamageEnemy"); // 충돌이 끝난 경우, InvokeRepeating() 함수를 취소합니다.
            currentEnemy = null; // 현재 적을 추적하는 변수를 초기화합니다.
        }
    }

    void DamageEnemy()
    {
        if (currentEnemy != null)
        {
            Debug.Log(damage);
            currentEnemy.TakeDamage(damage);
        }
    }

}