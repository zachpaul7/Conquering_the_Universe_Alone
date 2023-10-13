using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public Vector2 inputVec;

    Rigidbody2D rigid;
    Animator anim;

    private bool isTouchTop;
    private bool isTouchLeft;
    private bool isTouchRight;
    private bool isTouchBottom;

    public bool[] joyControl;
    public bool isControl;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
    }
    public void Move()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (joyControl[0]) { inputVec.x = -1; inputVec.y = 1; }
        if (joyControl[1]) { inputVec.x = 0; inputVec.y = 1; }
        if (joyControl[2]) { inputVec.x = 1; inputVec.y = 1; }
        if (joyControl[3]) { inputVec.x = -1; inputVec.y = 0; }
        if (joyControl[4]) { inputVec.x = 0; inputVec.y = 0; }
        if (joyControl[5]) { inputVec.x = 1; inputVec.y = 0; }
        if (joyControl[6]) { inputVec.x = -1; inputVec.y = -1; }
        if (joyControl[7]) { inputVec.x = 0; inputVec.y = -1; }
        if (joyControl[8]) { inputVec.x = 1; inputVec.y = -1; }

        if ((isTouchRight && inputVec.x == 1) || (isTouchLeft && inputVec.x == -1) || !isControl)
        {
            inputVec.x = 0;
        }

        if ((isTouchTop && inputVec.y == 1) || (isTouchBottom && inputVec.y == -1) || !isControl)
        {
            inputVec.y = 0;
        }
        /*        if ((isTouchRight && inputVec.x == 1) || (isTouchLeft && inputVec.x == -1))
                {
                    inputVec.x = 0;
                }

                if ((isTouchTop && inputVec.y == 1) || (isTouchBottom && inputVec.y == -1))
                {
                    inputVec.y = 0;
                }*/
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

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        anim.SetInteger("Input", (int)inputVec.magnitude);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top" :
                    isTouchTop = true;
                    break;

                case "Left":
                    isTouchLeft = true;
                    break;

                case "Right":
                    isTouchRight = true;
                    break;

                case "Bottom":
                    isTouchBottom = true;
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;

                case "Left":
                    isTouchLeft = false;
                    break;

                case "Right":
                    isTouchRight = false;
                    break;

                case "Bottom":
                    isTouchBottom = false;
                    break;
            }
        }
    }
}
