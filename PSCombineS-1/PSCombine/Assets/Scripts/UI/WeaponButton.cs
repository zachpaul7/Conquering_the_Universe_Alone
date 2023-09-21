using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButton : MonoBehaviour
{
    [SerializeField]
    Image icon;
    public UpgradeData upgradeData;
    public UpgradeController upgradeController; // Ãß°¡

    public void SetIcon(UpgradeData data)
    {
        upgradeData = data;
        icon.sprite = data.wpIcon;
    }

    public void OnClick()
    {
        if (upgradeData != null)
        {
            upgradeController.ApplyUpgrade(upgradeData);
            Debug.Log("Applying upgrade: " + upgradeData);
        }
    }

    public UpgradeData GetUpgradeData()
    {
        return upgradeData;
    }
}
