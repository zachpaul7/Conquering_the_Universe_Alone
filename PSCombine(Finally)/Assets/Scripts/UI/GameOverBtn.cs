using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBtn : MonoBehaviour
{
    public void Over()
    {
        GameManager.instance.soundManager.StopBGM();
        SceneManager.LoadScene("MainMenu");
    }
}