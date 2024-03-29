using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<Vector3> positionVectorList;
    public Transform target;
    int currentPathIndex;
    
    //Stats
    public int health;
    public int damage;
    public float speed;

    private void Awake()
    {
        EnemyList.enemies.Add(gameObject);
    }

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

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EnemyList.enemies.Remove(gameObject);
        Destroy(gameObject);
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

        if (target != null)
        {
            SetTargetPosition(target.position);
        }
    }
}
