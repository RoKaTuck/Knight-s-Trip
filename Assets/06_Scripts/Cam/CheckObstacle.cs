using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 direction = (transform.root.position - transform.position).normalized;

        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, Mathf.Infinity,
                            1 << LayerMask.NameToLayer("EnvironmentObject")); // 장애물 체크

        for (int i = 0; i < hits.Length; i++) // 장애물들에 부착되어있는 컴포넌트 참조하여 투명화 진행.
        {
            TransparentsObject[] obj = hits[i].transform.GetComponentsInChildren<TransparentsObject>();

            for (int j = 0; j < obj.Length; j++)
            {
                obj[j]?.BecomeTransparent();
            }
        }
    }
}
