using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;
    private GameObject player;

    private Transform target;
    public Transform minSpawn, maxSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnCounter = timeToSpawn;

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0)
        {
            spawnCounter = timeToSpawn;

            Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
        }

        transform.position = target.position;
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

}
