using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGM : MonoBehaviour
{
    public GameManager gm;
    void Awake()
    {
        GameManager gm = GameObject.Find("---GameManager---")?.GetComponent<GameManager>(); // Safe navigation operator (?)를 사용하여 null이 아닌 경우에만 GetComponent를 호출합니다.
        if (gm != null)
        {
            Destroy(gm.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
