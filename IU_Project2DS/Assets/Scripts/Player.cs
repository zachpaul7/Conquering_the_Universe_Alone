using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AllStats stats;
    
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;

    public bool[] joyControl;
    public bool isControl;
    public bool isButtonA;

    public float speed;
    public int power;

    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    public void JoyPanel(int type)
    {
        for (int index = 0; index < 9; index++)
        {
            joyControl[index] = index == type;
        }
    }

    public void JoyDown()
    {
        isControl = true;
    }
    public void JoyUp()
    {
        isControl = false;
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal"); // h ������ ���� ������ �Է� ���� �����մϴ�.
        float v = Input.GetAxisRaw("Vertical"); // v ������ ���� ������ �Է� ���� �����մϴ�.

        // ���̽�ƽ ����
        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }


        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1 || !isControl)) // Player ������ ����
            h = 0;

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1 || !isControl))
            v = 0;

        Vector3 curPos = transform.position; // curPos ������ ���� ��ġ ���� �����մϴ� 
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos; // transform.position�� ���ο� ��ġ ���� �����Ͽ� �÷��̾ �̵�

    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    public void ButtonADown()
    {
        isButtonA = true;
    }
    public void ButtonAUp()
    {
        isButtonA = false;
    }


    void Fire()
    {
        if (!isButtonA)
            return;

        if (curShotDelay < maxShotDelay) // �Ѿ��� �������� �߻����� ���ϰ� �ϱ� ���� ����
            return;

        switch (power)
        {
            // power�� 1�϶� ����Ǵ� �ڵ�
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse); // ���� �������� 10�� ���� ���� �߻�
                break;

            // power�� 2�϶� ����Ǵ� �ڵ� �⺻ �Ѿ��� 2�� �߻��Ŵ
            case 2:
                GameObject bulletR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.1f, transform.rotation);
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;
                GameObject bulletL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.1f, transform.rotation);
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            // power�� 3�϶� ����Ǵ� �ڵ� power2 ���� ������ �Ѿ��� ����� �߰���
            default:
                break;
        }

        curShotDelay = 0;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border") // Border �±׿� �����ϴ� ���� true
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border") // Border �±׿� ������ �����Ǵ� ���� false
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
