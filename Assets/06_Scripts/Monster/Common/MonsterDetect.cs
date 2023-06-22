using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDetect : MonoBehaviour
{    
    public bool _inSight = false;
    
    public float _viewAngle; // 시야 각도    
    public float _viewDistance; // 시야 거리

    public bool View(ref Transform hitTr)
    {
        // 정면 기준으로 시야 각의 절반만큼 왼쪽으로 회전한 방향 (좌측 경계선)
        Vector3 leftBoundary = BoundaryAngle(-this._viewAngle * 0.5f);
        // 정면 기준응로 시야 각의 절반만큼 오른쪽으로 회전한 방향 (우측 경계선)
        Vector3 rightBoundary = BoundaryAngle(this._viewAngle * 0.5f);

        bool _findPlayer = false;

        Debug.DrawRay(transform.position + transform.up, leftBoundary * _viewDistance, Color.red);
        Debug.DrawRay(transform.position + transform.up, rightBoundary * _viewDistance, Color.red);

        Collider[] targets = new Collider[5] ;
        Physics.OverlapSphereNonAlloc(transform.position, _viewDistance, targets ,1 << 6);
        
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] == null)
                break;

            Transform targetTf = targets[i].transform;

            if (targetTf.gameObject.layer == 6)
            {
                Vector3 direction = (targetTf.position - transform.position);
                float angle = Vector3.Angle(direction, transform.forward);

                if (angle < _viewAngle * 0.5f)
                {
                    RaycastHit hit;

                    if (Physics.Raycast(transform.position + transform.up, direction,
                                       out hit, _viewDistance))
                    {
                        if (hit.collider.gameObject.layer == 6)
                        {
                            _findPlayer = true;
                            hitTr = hit.transform;
                            //Debug.Log("플레이어 발견");
                            Debug.DrawRay(transform.position + transform.up, direction * _viewDistance, Color.blue);
                            return _findPlayer;
                        }
                    }                    
                }                
                    
            }
        }

        _findPlayer = false;        
        return _findPlayer;
    }

    public bool DetectForWakeUp(Vector3 originPos, float radius)
    {        
        Collider[] cols = Physics.OverlapSphere(originPos, radius, 1 << 6);

        if (cols.Length > 0)
            _inSight = true;        

        return _inSight;
    }

    private Vector3 BoundaryAngle(float angle)
    {
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f,
                           Mathf.Cos(angle * Mathf.Deg2Rad));        
    }
}
