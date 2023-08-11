using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public PlayerMove playerMove;
    public ObjectManager objectManager;

    void Awake()
    {
        instance = this;
    }
}
