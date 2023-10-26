using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeText : MonoBehaviour
{
    [SerializeField]
    Text textName;
    [SerializeField]
    Text textDetail;

    public void Set(UpgradeData upgradeData)
    {
        textName.text = upgradeData.ex;
        textDetail.text = upgradeData.detail;
    }

    internal void Clean()
    {
        textName.text = null;
        textDetail.text = null;
    }
}

