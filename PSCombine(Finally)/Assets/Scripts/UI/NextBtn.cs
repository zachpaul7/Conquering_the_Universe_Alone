using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBtn : MonoBehaviour
{
    [SerializeField] private GameObject[] scenes; // 씬들을 저장할 배열
    [SerializeField] private int currentSceneIndex = 0; // 현재 활성화된 씬의 인덱스

    public GameObject ManualPanel;

    private void Update()
    {
        ActivateScene(currentSceneIndex);
    }

    public void NextScene()
    {
        // 현재 활성화된 씬을 비활성화
        DeactivateScene(currentSceneIndex);

        currentSceneIndex++;

        if (currentSceneIndex >= scenes.Length)
        {
            currentSceneIndex = 0;
            ManualPanel.SetActive(false);

            return;
        }

    }

    private void ActivateScene(int index)
    {
        if (index >= 0 && index < scenes.Length)
        {
            scenes[index].SetActive(true);
        }
    }

    private void DeactivateScene(int index)
    {
        if (index >= 0 && index < scenes.Length)
        {
            scenes[index].SetActive(false);
        }
    }
}
