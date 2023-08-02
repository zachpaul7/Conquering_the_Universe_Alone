using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject UpgradePanel;


    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1;

    }
}
