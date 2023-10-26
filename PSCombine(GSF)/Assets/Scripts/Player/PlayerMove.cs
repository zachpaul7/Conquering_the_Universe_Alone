using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joy;

    [SerializeField] public float speed;
    [SerializeField] public Vector2 inputVec;

    Rigidbody2D rigid;
    Animator anim;

    public bool isTouchTop;
    public bool isTouchLeft;
    public bool isTouchRight;
    public bool isTouchBottom;

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
        float x = joy.Horizontal;
        float y = joy.Vertical;

        inputVec = new Vector2(x, y);


        Debug.Log("normal" + inputVec.x);

        if ((isTouchRight && inputVec.x > 0) || (isTouchLeft && inputVec.x < 0))
        {
            inputVec.x = 0;
        }

        if ((isTouchTop && inputVec.y > 0) || (isTouchBottom && inputVec.y < 0))
        {
            inputVec.y = 0;
        }

    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec);

        anim.SetInteger("Input", (int)inputVec.magnitude);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;

                    break;

                case "Left":
                    isTouchLeft = true;
                    Debug.Log("left" + inputVec.x);
                    break;

                case "Right":
                    isTouchRight = true;
                    Debug.Log("right" + inputVec.x);
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
