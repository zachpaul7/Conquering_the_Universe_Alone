using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBtn : MonoBehaviour
{
    public GameObject ManualPanel;
    public void OpenPanel()
    {
        ManualPanel.SetActive(true);
    }
}
