using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBtn : MonoBehaviour
{
    [SerializeField] private GameObject[] scenes; // ������ ������ �迭
    [SerializeField] private int currentSceneIndex = 0; // ���� Ȱ��ȭ�� ���� �ε���

    public GameObject ManualPanel;

    private void Update()
    {
        ActivateScene(currentSceneIndex);
    }

    public void NextScene()
    {
        // ���� Ȱ��ȭ�� ���� ��Ȱ��ȭ
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
