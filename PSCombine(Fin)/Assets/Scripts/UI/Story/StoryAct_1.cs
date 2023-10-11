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
        if (!isActive_s1)  //터치시 추가
        {
            scene[0].SetActive(false);
            scene[1].SetActive(true);
            isActive_s1 = true;

            StartCoroutine(Take2());
            StopCoroutine(Take2());
        }
        else if (!isActive_s2)  //터치시 추가
        {
            isActive_s2 = true;

            StartCoroutine(Take3());
            StopCoroutine(Take3());
        }
        else if (!isActive_s3) //터치시 추가
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

    IEnumerator Take1()
    {
        string[] strings = new string[3]{ "우주력 199년 9월 12일",
                                          "우리 은하 외곽 어딘가...",
                                          "이름 모를 행성"};

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
        string[] strings = new string[3]{ "메이데이...!  메이데이...!",
                                          "미확인 적기에 의해 공격받고 있다...!",
                                          "증원이 필요하ㄷ......"};

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
        string[] strings = new string[4]{ "... 여기는 사령부, 상황이 급박하다..!",
                                          "우주 전역에서 미확인 적기 출현! 아군이 공격받고 있다!",
                                          "제 83 특수임무부대에게는 적 정찰 임무를 부여한다...",
                                          "적의 규모를 파악하고 모선을 발견하는 즉시 파괴하라..."};

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
