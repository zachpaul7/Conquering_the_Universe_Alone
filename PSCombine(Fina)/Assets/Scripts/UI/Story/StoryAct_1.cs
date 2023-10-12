using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryAct_1 : MonoBehaviour
{
    [SerializeField] private GameObject skipBtn;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject[] scene;
    

    [SerializeField] private Text[] introTexts;
    [SerializeField] private Text[] textBox;

    public SoundManager soundManager;

    private bool isActive_s1 = true;
    private bool isActive_s2 = true;
    private bool isActive_s3 = true;

    private void Start()
    {
        soundManager.PlayBGM("Story Act");
        skipBtn.SetActive(false);
        dialog.SetActive(false);
        scene[0].SetActive(true);

        for(int i = 1; i < scene.Length; i++)
        {
            scene[i].SetActive(false);
        }

        StartCoroutine(Take1());
        StopCoroutine(Take1());
    }

    private void Update()
    {
        if (!isActive_s1)  //��ġ�� �߰�
        {
            scene[0].SetActive(false);
            scene[1].SetActive(true);
            isActive_s1 = true;

            StartCoroutine(Take2());
            StopCoroutine(Take2());
        }
        else if (!isActive_s2)  //��ġ�� �߰�
        {
            isActive_s2 = true;

            StartCoroutine(Take3());
            StopCoroutine(Take3());
        }
        else if (!isActive_s3) //��ġ�� �߰�
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    IEnumerator Take1()
    {
        string[] strings = new string[3]{ "���ַ� 199�� 9�� 12��",
                                          "�츮 ���� �ܰ� ���...",
                                          "�̸� �� �༺"};

        foreach (Text t in introTexts)
            t.text = "";

        for (int t = 0; t < introTexts.Length && t < strings.Length; t++)
        {
            int strTypingLength = strings[t].GetTypingLength();

            for (int i = 0; i <= strTypingLength; i++)
            {
                introTexts[t].text = strings[t].Typing(i);
                yield return YieldCache.WaitForSeconds(0.06f);
            }

            yield return YieldCache.WaitForSeconds(1f);
        }

        yield return YieldCache.WaitForSeconds(1.5f);

        isActive_s1 = false;
    }

    IEnumerator Take2()
    {
        string[] strings = new string[3]{ "���̵���...!  ���̵���...!",
                                          "��Ȯ�� ���⿡ ���� ���ݹް� �ִ�...!",
                                          "������ �ʿ��Ϥ�......"};

        foreach (Text t in textBox)
            t.text = "";

        yield return YieldCache.WaitForSeconds(2f);

        dialog.SetActive(true);
        skipBtn.SetActive(true);

        for (int t = 0; t < textBox.Length && t < strings.Length; t++)
        {
            int strTypingLength = strings[t].GetTypingLength();

            for (int i = 0; i <= strTypingLength; i++)
            {
                textBox[t].text = strings[t].Typing(i);
                yield return YieldCache.WaitForSeconds(0.05f);
            }

            if (t >= 2)
            {
                scene[1].SetActive(false);
                scene[2].SetActive(true);
            }
            yield return YieldCache.WaitForSeconds(1.5f);
        }

        isActive_s2 = false;
        dialog.SetActive(false);
        scene[2].SetActive(false);
        scene[3].SetActive(true);

        yield return YieldCache.WaitForSeconds(2f);
    }

    IEnumerator Take3()
    {
        string[] strings = new string[4]{ "... ����� ��ɺ�, ��Ȳ�� �޹��ϴ�..!",
                                          "���� �������� ��Ȯ�� ���� ����! �Ʊ��� ���ݹް� �ִ�!",
                                          "�� 83 Ư���ӹ��δ뿡�Դ� �� ���� �ӹ��� �ο��Ѵ�...",
                                          "���� �Ը� �ľ��ϰ� ���� �߰��ϴ� ��� �ı��϶�..."};

        foreach (Text t in textBox)
            t.text = "";

        yield return YieldCache.WaitForSeconds(4f);
        scene[3].SetActive(false);
        scene[4].SetActive(true);
        dialog.SetActive(true);

        for (int t = 0; t < textBox.Length && t < strings.Length; t++)
        {
            int strTypingLength = strings[t].GetTypingLength();

            for (int i = 0; i <= strTypingLength; i++)
            {
                textBox[t].text = strings[t].Typing(i);
                yield return YieldCache.WaitForSeconds(0.05f);
            }

            yield return YieldCache.WaitForSeconds(1.5f);
        }
        
        yield return YieldCache.WaitForSeconds(4f);
        isActive_s3 = false;
    }

    public void SkipButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
