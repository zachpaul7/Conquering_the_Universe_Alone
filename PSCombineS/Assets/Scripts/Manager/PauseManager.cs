using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    private void Start()
    {
        upgradePanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (upgradePanel == null)
        {
            upgradePanel = GameObject.Find("Canvas").transform.GetChild(0).gameObject;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;

    }
}
