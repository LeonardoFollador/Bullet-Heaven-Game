using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    private GameObject player;

    private Transform target;
    public Transform minSpawn, maxSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    public int checkPerFrame;
    private int enemyToCheck;

    public List<WaveInfo> waves;
    private int currentWave;
    private float waveCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCounter = timeToSpawn;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.GetComponent<Transform>();
        }

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 7f;

        currentWave--;
        GoToNextWave();
    }

    // Update is called once per frame
    void Update()
    {
        /*spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);

            spawnedEnemies.Add(newEnemy);
        }*/

        if (PlayerHealth.instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    GoToNextWave();
                }

                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        if (target != null)
        {
            transform.position = target.position;
        }

        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
            else if (Vector3.Distance(transform.position, spawnedEnemies[i].transform.position) > despawnDistance)
            {
                Destroy(spawnedEnemies[i]);

                spawnedEnemies.RemoveAt(i);
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.x = minSpawn.position.x;
            }
            else
            {
                spawnPoint.x = maxSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.y = minSpawn.position.y;
            }
            else
            {
                spawnPoint.y = maxSpawn.position.y;
            }
        }

        return spawnPoint;
    }

    public void GoToNextWave()
    {
        currentWave++;

        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }
}

[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
