using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] public float fireRate; // 발사 속도 (총알 사이의 간격)

    void Start()
    {
        StartCoroutine("FireCoroutine"); // 코루틴 시작: 반복적으로 발사 메서드를 호출합니다.
    }

    private void Update()
    {
        if (GameManager.instance.isWork)
        {
            StartCoroutine("FireCoroutine");
            GameManager.instance.isWork = false;
        }
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire(); // 발사 메서드 호출로 총알을 생성하고 발사합니다.
            GameManager.instance.soundManager.PlaySFX("NormalAttack");
            yield return new WaitForSeconds(fireRate); // 다음 발사까지 대기합니다.
        }

    }

    void Fire()
    {
        if (GameManager.instance.objectManager)
        {
            if (GameManager.instance.playerPower == 1)
            {
                // 플레이어 캐릭터의 위치와 회전값에 따라 총알을 생성합니다.
                GameObject bullet = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
                bullet.transform.position = GameManager.instance.playerMove.transform.position;

                // 총알의 Rigidbody2D 컴포넌트를 가져와 위쪽으로 힘을 가해 이동시킵니다.
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
            if (GameManager.instance.playerPower == 2)
            {
                GameObject bulletR = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
                GameObject bulletL = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");

                bulletR.transform.position = GameManager.instance.playerMove.transform.position + Vector3.up * 0.01f + Vector3.right * 0.2f;
                bulletL.transform.position = GameManager.instance.playerMove.transform.position + Vector3.left * 0.2f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
            if (GameManager.instance.playerPower == 3)
            {
                GameObject bulletR = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
                GameObject bulletC = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
                GameObject bulletL = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");

                bulletR.transform.position = GameManager.instance.playerMove.transform.position + Vector3.up * 0.01f + Vector3.right * 0.3f;
                bulletC.transform.position = GameManager.instance.playerMove.transform.position + Vector3.up * 0.2f;
                bulletL.transform.position = GameManager.instance.playerMove.transform.position + Vector3.left * 0.3f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidC = bulletC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }

        }
    }
}
