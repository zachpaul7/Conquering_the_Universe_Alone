using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Scriptable Object/stat")]
public class AllStats : ScriptableObject
{
    public int bulletDmg;
    public int health;
    public float speed;
    public string objectName;
}
