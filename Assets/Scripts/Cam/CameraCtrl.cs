using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;  // 따라다닐 대상 플레이어의 Transform 컴포넌트

    public float smoothSpeed = 0.125f;  // 카메라 이동 스무딩 정도
    public Vector3 offset;  // 플레이어와의 상대적인 거리   

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.localPosition + offset;  // 플레이어의 위치에 오프셋을 더하여 목표 위치 계산
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // 현재 위치에서 목표 위치까지 부드럽게 이동

        transform.position = smoothedPosition;  // 카메라 위치를 업데이트
    }
}
