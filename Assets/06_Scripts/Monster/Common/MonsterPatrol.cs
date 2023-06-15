using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPatrol : MonoBehaviour
{
    private Transform[] patrolPoints; // ���� ��� ������
    private int currentPoint = 0; // ���� ��� �ε���

    //private bool _canMove;
    private bool movingRight = true; // ���� �̵� ����

    public float patrolDistance = 3f; // �¿�� �̵��� �Ÿ�       
    public float _validStopDistance;

    private void Awake()
    {
        // ������ �ν��Ͻ��� ���� ��� �������� ã�Ƽ� �Ҵ�
        patrolPoints = transform.GetComponentsInChildren<Transform>();

        // ��� ���� �迭���� ���� ��ġ�� �ε����� ã��
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (patrolPoints[i] == transform)
            {
                currentPoint = i;
                break;
            }
        }
    }

    public void MoveDestination(NavMeshAgent agent, out bool canMove)
    {
        if (agent.remainingDistance <= Mathf.Pow(_validStopDistance, 2))
        {
            //_canMove = false;
            canMove = false;
        }
        else
        {
            //_canMove = true;
            canMove = true;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {            
            if (movingRight)
            {
                currentPoint = (currentPoint + 1) % patrolPoints.Length;

                if (currentPoint == 0)
                    movingRight = false;
            }
            else
            {
                currentPoint = (currentPoint - 1 + patrolPoints.Length) % patrolPoints.Length;

                if (currentPoint == patrolPoints.Length - 1)
                    movingRight = true;
            }

            Vector3 nextPosition = GetNextPosition();
            SetDestination(agent, nextPosition);
        }        
    }
    public void SetDestination(NavMeshAgent agent, Vector3 destination)
    {
        if (agent != null)
        {
            agent.SetDestination(destination);
        }
    }

    public void PatrolStart(NavMeshAgent agent)
    {
        if (patrolPoints.Length > 0)
        {
            SetDestination(agent, patrolPoints[currentPoint].position);
        }
    }

    public bool ResetPosition(NavMeshAgent agent, Vector3 originPos)
    {
        //_canMove = true;
        //canMove = _canMove;
        SetDestination(agent, originPos);
               
        if (agent.remainingDistance <= 0.5f )
        {            
            currentPoint = 0;
            SetDestination(agent, patrolPoints[currentPoint].position);

            return true;
        }

        return false;
    }

    private Vector3 GetNextPosition()
    {
        Vector3 nextPosition = patrolPoints[currentPoint].position;

        if (!movingRight)
        {
            nextPosition -= transform.right * patrolDistance;
        }
        else
        {
            nextPosition += transform.right * patrolDistance;
        }

        return nextPosition;
    }

}
