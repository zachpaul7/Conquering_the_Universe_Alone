using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public int bfgDmg;
    public int baseDmg;
    public int rocketDmg;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeBfg()
    {
        bfgDmg = 300;
    }
    public void UpgradeBase()
    {
        baseDmg = 20;
    }
    public void UpgradeRocket()
    {
        rocketDmg = 40;
    }
}
