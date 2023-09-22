using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField]GameObject SplashObj;               //�ǳڿ�����Ʈ

    [SerializeField]Image image;                            //�ǳ� �̹���



    private bool checkbool = false;     //���� ���� ���� ����



    void Awake()

    {

        SplashObj = this.gameObject;                         //��ũ��Ʈ ������ ������Ʈ

        image = SplashObj.GetComponent<Image>();    //�ǳڿ�����Ʈ�� �̹��� ����

    }



    void Update()

    {

        StartCoroutine("MainSplash");                        //�ڷ�ƾ    //�ǳ� ���� ����

        if (checkbool)                                            //���� checkbool �� ���̸�

        {

            Destroy(this.gameObject);                        //�ǳ� �ı�, ����

        }

    }



    IEnumerator MainSplash()

    {

        Color color = image.color;                            //color �� �ǳ� �̹��� ����



        for (int i = 100; i >= 0; i--)                            //for�� 100�� �ݺ� 0���� ���� �� ����

        {

            color.a -= Time.deltaTime * 0.01f;               //�̹��� ���� ���� Ÿ�� ��Ÿ �� * 0.01



            image.color = color;                                //�ǳ� �̹��� �÷��� �ٲ� ���İ� ����



            if (image.color.a <= 0)                        //���� �ǳ� �̹��� ���� ���� 0���� ������

            {

                checkbool = true;                              //checkbool �� 

            }

        }

        yield return null;                                        //�ڷ�ƾ ����

    }
}
