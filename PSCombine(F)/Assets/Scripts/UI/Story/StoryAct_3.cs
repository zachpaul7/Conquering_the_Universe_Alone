using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryAct_3 : MonoBehaviour
{
    [SerializeField] private GameObject skipBtn;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject[] scene;

    [SerializeField] private Text[] textBox;

    private bool isActive_s1 = true;
    private bool isActive_s2 = true;

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
            scene[1].SetActive(false);
            scene[2].SetActive(true);
            isActive_s1 = true;

            StartCoroutine(Take2());
            StopCoroutine(Take2());
        }
        
        else if (!isActive_s2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    IEnumerator Take1()
    {
        string[] strings = new string[4]{ "수고했네, 이제 다 끝난것 같군.",
                                          "삐용 삐용",
                                          "!!!!",
                                          "무슨 일이지??"};

        foreach (Text t in textBox)
            t.text = "";

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

            yield return YieldCache.WaitForSeconds(1.5f);

            switch (t)
            {
                case 0:
                    scene[0].SetActive(false);
                    scene[1].SetActive(true);
                    break;
            }
        }

        yield return YieldCache.WaitForSeconds(2f);

        isActive_s1 = false;
    }

    IEnumerator Take2()
    {
        string[] strings = new string[5]{ "키킥, 이정도로 끝날거라고 생각했나?",
                                          "누구냐!!!",
                                          "너희의 전투력 분석이 끝났다. 키키킥",
                                          "!!!!!!!!!!",
                                          "자! 이제 최후의 대결을 시작해 보자고 키킥"};

        foreach (Text t in textBox)
            t.text = "";


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
                    scene[2].SetActive(false);
                    scene[3].SetActive(true);
                    break;
                case 1:
                    scene[3].SetActive(false);
                    scene[4].SetActive(true);
                    break;
                case 2:
                    scene[4].SetActive(false);
                    scene[5].SetActive(true);
                    break;
                case 3:
                    scene[5].SetActive(false);
                    scene[4].SetActive(true);
                    break;
            }
        }
        
        yield return YieldCache.WaitForSeconds(2f);

        isActive_s2 = false;
    }


    public void SkipButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
