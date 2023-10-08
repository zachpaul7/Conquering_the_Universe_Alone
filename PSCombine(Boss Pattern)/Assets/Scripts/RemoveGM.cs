using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveGM : MonoBehaviour
{
    public GameManager gm;
    void Awake()
    {
        GameManager gm = GameObject.Find("---GameManager---")?.GetComponent<GameManager>(); // Safe navigation operator (?)�� ����Ͽ� null�� �ƴ� ��쿡�� GetComponent�� ȣ���մϴ�.
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
