using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtn : MonoBehaviour
{
    public GameObject OptionPanel;
    PauseManager pauseManager;
    GameManager gameManager;
    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
    }

    public void OpenOption()
    {
        if (pauseManager != null)
        {
            pauseManager.PauseGame();
        }
        OptionPanel.SetActive(true);
    }

    public void CloseOption()
    {
        if (pauseManager != null)
        {
            pauseManager.UnPauseGame();
        }
        OptionPanel.SetActive(false);
    }
    public void GoMain()
    {

        SceneManager.LoadScene("MainMenu");
        if (pauseManager != null)
        {
            pauseManager.UnPauseGame();
        }
        OptionPanel.SetActive(false);
        
        
    }
}
