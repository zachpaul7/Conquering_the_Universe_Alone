using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public float fireRate = 0.3f; // �߻� �ӵ� (�Ѿ� ������ ����)
    

    void Start()
    {
        StartCoroutine("FireCoroutine"); // �ڷ�ƾ ����: �ݺ������� �߻� �޼��带 ȣ���մϴ�.
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
        // �÷��̾� ĳ������ ��ġ�� ȸ������ ���� �Ѿ��� �����մϴ�.
        GameObject bullet = GameManager.instance.objectManager.MakeObj("Bullet_Player_Cannon");
        bullet.transform.position = GameManager.instance.playerMove.transform.position;

        // �Ѿ��� Rigidbody2D ������Ʈ�� ������ �������� ���� ���� �̵���ŵ�ϴ�.
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
    }
}
