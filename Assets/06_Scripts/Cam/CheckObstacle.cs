using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 direction = (transform.root.position - transform.position).normalized;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
                            1 << LayerMask.NameToLayer("EnvironmentObject")); // ��ֹ� üũ

        for (int i = 0; i < hits.Length; i++) // ��ֹ��鿡 �����Ǿ��ִ� ������Ʈ �����Ͽ� ����ȭ ����.
        {
            TransparentsObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentsObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
