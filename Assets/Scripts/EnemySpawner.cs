using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private const float SPEEDMAX = 0.5f;
    private float spawnSpeed = 3f;

    private Enemy[] enemies;

    private float spawnDelay = 2f;
    private int spawn = 0;

    void Start()
    {

        enemies = gameObject.GetComponentsInChildren<Enemy>();
        foreach(Enemy e in enemies)
        {
            e.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnDelay -= Time.deltaTime;
        if(spawnDelay < 0)
        {
            Spawn();
            spawnDelay = spawnSpeed;

        }
        if(spawn > 5)
        {
            spawnSpeed -= 0.1f;
            spawn = 0;
            if(spawnSpeed < SPEEDMAX)
            {
                spawnSpeed = SPEEDMAX;
            }
        }

    }

    private void Spawn()
    {
        Enemy e = enemies[Random.Range(0, enemies.Length - 1)];
        Enemy e2 = enemies[Random.Range(0, enemies.Length - 1)];

        Enemy enemy = Instantiate(e, transform);
        enemy.transform.position = e2.gameObject.transform.position;
        enemy.gameObject.SetActive(true);
        spawn++;
    }

}
