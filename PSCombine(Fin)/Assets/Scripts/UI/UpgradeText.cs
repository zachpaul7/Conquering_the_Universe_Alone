using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    [SerializeField]
    Text textEx;
    public void Set(UpgradeData upgradeData)
    {
        textEx.text = upgradeData.ex;
    }

    internal void Clean()
    {
        textEx.text = null;
    }
}

