using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab;
    public float firstSpawnTime = 40f; // primeiro boss (40 sec)
    public float spawnInterval = 60f;   // depois a cada 1 min

    private float timer = 0f;
    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = firstSpawnTime;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSpawnTime)
        {
            SpawnBoss();
            nextSpawnTime += spawnInterval;
        }
    }

    void SpawnBoss()
    {
        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 offset = Random.insideUnitCircle.normalized * 10f;

        return player.transform.position + offset;
    }
}