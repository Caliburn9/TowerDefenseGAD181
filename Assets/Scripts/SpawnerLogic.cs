using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLogic : MonoBehaviour
{
    public int spawnAmount;
    public GameObject enemyObject;
    public Transform spawnLocation, targetLocation;
    public float timer;
    float time = 0.0f;
    float minTimer, maxTimer;
    
    void SpawnEnemy(GameObject enemy, Transform spawnLocation)
    {
        Instantiate(enemy, spawnLocation.transform.position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        minTimer = timer - 2;
        maxTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= timer)
        {
            time = 0.0f;
            timer = Random.Range(minTimer, maxTimer);

            if (spawnAmount != 0)
            {
                SpawnEnemy(enemyObject, spawnLocation);
                spawnAmount--;
                enemyObject.GetComponent<EnemyAI>().target = targetLocation;
            }
        }
    }
}
