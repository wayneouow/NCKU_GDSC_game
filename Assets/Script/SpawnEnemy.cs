using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public float SpawnCoolDown = 5f;
    public bool canSpawn = true;
    public GameObject spawnBaseEffect; 
    public GameObject spawnEnemyEffect;
    public float spawnRad = 10f;
    public List<GameObject> enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            GameObject en = enemy[Random.Range(0, enemy.Count)];
            canSpawn = false;
            Instantiate(spawnBaseEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-spawnRad, spawnRad), transform.position.y + 1f, transform.position.z + Random.Range(-spawnRad, spawnRad));
            Instantiate(spawnEnemyEffect, pos, transform.rotation);
            Instantiate(en, pos, transform.rotation);
            Invoke(nameof(SpawnReset), SpawnCoolDown);
        }
    }

    void SpawnReset()
    {
        canSpawn = true;
    }
}
