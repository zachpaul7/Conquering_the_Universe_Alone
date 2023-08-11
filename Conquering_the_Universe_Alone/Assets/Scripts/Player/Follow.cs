using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform playerTransform; // 플레이어의 위치를 저장할 Transform 변수

    void Update()
    {
        // 오브젝트의 위치를 플레이어의 위치로 업데이트
        transform.position = playerTransform.position;
    }
}
