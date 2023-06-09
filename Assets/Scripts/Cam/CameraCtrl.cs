using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Transform target;  // ����ٴ� ��� �÷��̾��� Transform ������Ʈ

    public float smoothSpeed = 0.125f;  // ī�޶� �̵� ������ ����
    public Vector3 offset;  // �÷��̾���� ������� �Ÿ�   

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.localPosition + offset;  // �÷��̾��� ��ġ�� �������� ���Ͽ� ��ǥ ��ġ ���
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);  // ���� ��ġ���� ��ǥ ��ġ���� �ε巴�� �̵�

        transform.position = smoothedPosition;  // ī�޶� ��ġ�� ������Ʈ
    }
}
