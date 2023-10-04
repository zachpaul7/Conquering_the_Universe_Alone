using KoreanTyper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoryAct_End : MonoBehaviour
{
    [SerializeField] private GameObject skipBtn;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject[] scene;

    [SerializeField] private Text[] textBox;

    private bool isActive_s1 = true;
    private bool isActive_s2 = true;
    private bool isActive_s3 = true;

    private void Start()
    {
        finishPanel.SetActive(false);
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
            scene[1].SetActive(true);
            dialog.SetActive(true);
            isActive_s1 = true;

            StartCoroutine(Take2());
            StopCoroutine(Take2());
        }
        else if (!isActive_s2)
        {
            scene[1].SetActive(false);
            scene[2].SetActive(true);
            dialog.SetActive(true);
            isActive_s2 = true;

            StartCoroutine(Take3());
            StopCoroutine(Take3());

        }
        else if (!isActive_s3)
        {
            SkipButton();
        }

    }

    IEnumerator Take1()
    {
        string[] strings = new string[5]{ "1 . . . . . . .?",
                                          "1 . . . . . . . . . .!",
                                          "1 . . . . . . . . . . .@",
                                          "1 . . . . . . . . . .#",
                                          "1 . . . . . . . . . . .$"};

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

            yield return YieldCache.WaitForSeconds(1.5f);
        }
        dialog.SetActive(false);
        yield return YieldCache.WaitForSeconds(2f);

        isActive_s1 = false;
    }

    IEnumerator Take2()
    {
        string[] strings = new string[4]{ "2 . . . . . . . . . . . . . .%",
                                          "2 . . . . . . . . . . . . .^",
                                          "2 . . . . . . . . . . .&",
                                          "2 . . . . . . . . . .*"};

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
        }
        dialog.SetActive(false);
        yield return YieldCache.WaitForSeconds(2f);

        isActive_s2 = false;
    }

    IEnumerator Take3()
    {
        string[] strings = new string[3]{ "3 . . . . . . .(",
                                          "3 . . . . . . . . . .)",
                                          "3 . . . . . . . . . . .+="};

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
        }

        yield return YieldCache.WaitForSeconds(4f);

        isActive_s3 = false;
    }

    public void SkipButton()
    {
        skipBtn.SetActive(false);
        finishPanel.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
