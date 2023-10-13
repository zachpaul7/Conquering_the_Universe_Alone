using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitBtn : MonoBehaviour
{
    public GameObject Panel;
    
    public void Skip()
    {
        Panel.SetActive(false);
    }
}
