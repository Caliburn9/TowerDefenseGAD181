using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float range, fireRate, force;
    public GameObject bullet;
    public Transform spawnLoc;
    float shootTimer;
    GameObject target;
    Vector2 direction;

    private void Start()
    {
        shootTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        TargetNearestEnemy();

        if (Time.time >= shootTimer)
        {
            if (target != null)
            {
                Vector2 targetpos = target.transform.position;
                direction = targetpos - (Vector2)transform.position;
                shootTimer = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        GameObject bulletInst = Instantiate(bullet, spawnLoc.transform.position, Quaternion.identity);
        bulletInst.GetComponent<Rigidbody2D>().AddForce(direction * force);
    }

    void TargetNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float distance = Mathf.Infinity;

        foreach(GameObject enemy in EnemyList.enemies)
        {
            if (enemy != null)
            {
                float _distance = (transform.position - enemy.transform.position).magnitude;

                if (_distance < distance)
                {
                    distance = _distance;
                    nearestEnemy = enemy;
                }
            }
        }

        if (distance <= range)
        {
            target = nearestEnemy;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    //              OLD SHOOTING LOGIC
    //bool inRange = false;
    //Vector2 direction;
    //Vector2 targetpos = target.position;
    //direction = targetpos - (Vector2)transform.position;
    //RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range);
    //if (rayInfo)
    //{
    //    if (rayInfo.collider.gameObject.tag == "Enemy")
    //    {
    //        if (inRange == false)
    //        {
    //            inRange = true;
    //        }
    //    }
    //    else
    //    {
    //        if (inRange == true)
    //        {
    //            inRange = false;
    //        }
    //    }
    //}
    //
    //if (inRange)
    //{
    //    if (Time.time > shootTimer)
    //    {
    //        shootTimer = Time.time / fireRate;
    //        Shoot();
    //    }
    //}
}
