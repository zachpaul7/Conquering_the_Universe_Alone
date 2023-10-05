using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.3f; // �߻� �ӵ� (�Ѿ� ������ ����)

    void Start()
    {
        StartCoroutine("FireCoroutine"); // �ڷ�ƾ ����: �ݺ������� �߻� �޼��带 ȣ���մϴ�.
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
            Fire(); // �߻� �޼��� ȣ��� �Ѿ��� �����ϰ� �߻��մϴ�.
            yield return new WaitForSeconds(fireRate); // ���� �߻���� ����մϴ�.
        }

    }

    void Fire()
    {
        if (GameManager.instance.objectManager)
        {
            if (GameManager.instance.playerPower == 1)
            {
                // �÷��̾� ĳ������ ��ġ�� ȸ������ ���� �Ѿ��� �����մϴ�.
                GameObject bullet = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
                bullet.transform.position = GameManager.instance.playerMove.transform.position;

                // �Ѿ��� Rigidbody2D ������Ʈ�� ������ �������� ���� ���� �̵���ŵ�ϴ�.
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }
            if (GameManager.instance.playerPower == 2)
            {
                GameObject bulletR = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
                GameObject bulletL = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");

                bulletR.transform.position = GameManager.instance.playerMove.transform.position + Vector3.up * 0.02f + Vector3.right * 0.2f;
                bulletL.transform.position = GameManager.instance.playerMove.transform.position + Vector3.left * 0.2f;

                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();

                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            }

        }
    }
}
