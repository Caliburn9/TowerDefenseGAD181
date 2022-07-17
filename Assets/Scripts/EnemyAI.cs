using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Vector3> positionVectorList;
    int currentPathIndex;
    float speed = 2.5f;

    private void Move()
    {
        if (positionVectorList != null)
        {
            Vector3 targetPosition = positionVectorList[currentPathIndex];

            if (Vector3.Distance(transform.position, targetPosition) > .01f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position = transform.position + moveDir * speed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if (currentPathIndex >= positionVectorList.Count)
                {
                    Stop();
                }
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        currentPathIndex = 0;
        positionVectorList = Pathfinding.Instance.FindPath(GetPosition(), targetPos);

        if (positionVectorList != null && positionVectorList.Count > 1)
        {
            positionVectorList.RemoveAt(0);
        }
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }

    private void Stop()
    {
        positionVectorList = null;
    }

    private void Start()
    {
        positionVectorList = null;
    }

    private void Update()
    {
        if (positionVectorList != null)
        {
            Move();
        }
    }
}
