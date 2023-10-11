using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUpgrade,
    StatsUpgrade,
    WeaponUnlock,
}
[CreateAssetMenu(fileName = "UpgradeData", menuName = "Scriptable Object/upgrade")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;
    public string Name;
    public Sprite icon;
    public Sprite wpIcon;
    public string ex;
}
