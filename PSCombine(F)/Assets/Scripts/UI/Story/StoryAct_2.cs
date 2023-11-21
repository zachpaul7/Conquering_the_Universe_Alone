using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryAct_2 : MonoBehaviour
{
    [SerializeField] private GameObject skipBtn;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject[] scene;

    [SerializeField] private Text[] textBox;

    private bool isActive_s1 = true;
    private bool isActive_s2 = true;
    private bool isActive_s3 = true;

    private void Start()
    {
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
        if (!isActive_s1)
        {
            scene[0].SetActive(false);
            scene[2].SetActive(true);

            isActive_s1 = true;

            StartCoroutine(Take2());
            StopCoroutine(Take2());
        }
        else if (!isActive_s2)
        {
            scene[4].SetActive(false);
            scene[1].SetActive(true);

            isActive_s2 = true;

            StartCoroutine(Take3());
            StopCoroutine(Take3());

        }
        else if (!isActive_s3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    IEnumerator Take1()
    {
        string[] strings = new string[4]{ "임무를 아주 훌륭하게 완수했군!",
                                          "하지만 아직 축하하긴 일러...",
                                          "적의 공격 원인과 목적을 알아야해.",
                                          "적의 파괴된 기지에 잠입해 정보를 알아오게나."};

        foreach (Text t in textBox)
            t.text = "";

        skipBtn.SetActive(true);
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
        
        yield return YieldCache.WaitForSeconds(2f);

        dialog.SetActive(false);

        isActive_s1 = false;
    }

    IEnumerator Take2()
    {
        string[] strings = new string[4]{ "저번 침공과는 다른 형태의 우주선...?",
                                          "이렇게 대규모 병력이 집결해 있다니...",
                                          "도당체 무슨일이 일어나고 있는거지?",
                                          "얼른 본부에 알려야 겠어..."};

        foreach (Text t in textBox)
            t.text = "";


        yield return YieldCache.WaitForSeconds(2f);
        scene[2].SetActive(false);
        scene[3].SetActive(true);

        yield return YieldCache.WaitForSeconds(2f);
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

        yield return YieldCache.WaitForSeconds(2f);

        dialog.SetActive(false);

        isActive_s2 = false;
    }

    IEnumerator Take3()
    {
        string[] strings = new string[4]{ "미확인 적의 대규모 병력이 파괴된 기지에 집결중입니다!",
                                          "흠... 그렇단 말이지...",
                                          "이번에도 같은 미션이다. 적의 병력을 교란하고 모선을 발견하는 즉시 파괴하라",
                                          "키키킥, 더 열심히 발버둥 처봐라."};

        foreach (Text t in textBox)
            t.text = "";

        yield return YieldCache.WaitForSeconds(2f);
        dialog.SetActive(true);
        scene[1].SetActive(false);
        scene[5].SetActive(true);

        for (int t = 0; t < textBox.Length && t < strings.Length; t++)
        {
            int strTypingLength = strings[t].GetTypingLength();

            for (int i = 0; i <= strTypingLength; i++)
            {
                textBox[t].text = strings[t].Typing(i);
                yield return YieldCache.WaitForSeconds(0.05f);
            }

            yield return YieldCache.WaitForSeconds(1.5f);

            switch (t)
            {
                case 0:
                    scene[5].SetActive(false);
                    scene[0].SetActive(true);
                    break;

                case 2:
                    yield return YieldCache.WaitForSeconds(2f);
                    dialog.SetActive(false);
                    scene[0].SetActive(false);
                    scene[2].SetActive(true);

                    yield return YieldCache.WaitForSeconds(2f);
                    dialog.SetActive(true);
                    scene[2].SetActive(false);
                    scene[6].SetActive(true);

                    break;
                case 3:
                    scene[5].SetActive(false);
                    scene[0].SetActive(true);
                    break;
            }
        }

        yield return YieldCache.WaitForSeconds(4f);

        isActive_s3 = false;
    }

    public void SkipButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
