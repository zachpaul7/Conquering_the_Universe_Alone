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
        float h = Input.GetAxisRaw("Horizontal"); // h 변수는 수평 방향의 입력 값을 저장합니다.
        float v = Input.GetAxisRaw("Vertical"); // v 변수는 수직 방향의 입력 값을 저장합니다.

        // 조이스틱 조작
        if (joyControl[0]) { h = -1; v = 1; }
        if (joyControl[1]) { h = 0; v = 1; }
        if (joyControl[2]) { h = 1; v = 1; }
        if (joyControl[3]) { h = -1; v = 0; }
        if (joyControl[4]) { h = 0; v = 0; }
        if (joyControl[5]) { h = 1; v = 0; }
        if (joyControl[6]) { h = -1; v = -1; }
        if (joyControl[7]) { h = 0; v = -1; }
        if (joyControl[8]) { h = 1; v = -1; }


        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1 || !isControl)) // Player 움직임 제한
            h = 0;

        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1 || !isControl))
            v = 0;

        Vector3 curPos = transform.position; // curPos 변수는 현재 위치 값을 저장합니다 
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos; // transform.position에 새로운 위치 값을 설정하여 플레이어를 이동

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

        if (curShotDelay < maxShotDelay) // 총알을 연속으로 발사하지 못하게 하기 위한 제한
            return;

        switch (power)
        {
            // power가 1일때 실행되는 코드
            case 1:
                GameObject bullet = Instantiate(bulletObjA, transform.position, transform.rotation);
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse); // 위쪽 방향으로 10의 힘을 가해 발사
                break;

            // power가 2일때 실행되는 코드 기본 총알을 2발 발사시킴
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

            // power가 3일때 실행되는 코드 power2 에서 강력한 총알을 가운데에 추가함
            default:
                break;
        }

        curShotDelay = 0;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border") // Border 태그와 접촉하는 순간 true
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
        if (collision.gameObject.tag == "Border") // Border 태그와 접촉이 해제되는 순간 false
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
