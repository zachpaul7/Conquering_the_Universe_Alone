using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public string eName;
    public float hp = 999f;
    public float speed = 1f;
    public float eDmg = 1f;
    public int spawnType = 0;
}

