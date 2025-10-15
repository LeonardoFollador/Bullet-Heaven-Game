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

    // --- PROPRIEDADES ADICIONADAS ---
    [Header("Spawn de Inimigos Antigos")]
    [Tooltip("Tempo em segundos entre as tentativas de spawn de inimigos de waves anteriores.")]
    public float oldEnemySpawnRate = 3.0f;
    private float oldEnemySpawnCounter;

    [Tooltip("Probabilidade (0 a 1) de um inimigo antigo ser spawnado no ciclo (menor = mais raro).")]
    public float oldEnemySpawnChance = 0.5f;

    // --- PROPRIEDADE PARA LIMITAÇÃO ---
    [Header("Limitação")]
    private const int MAX_ENEMIES = 30;
    // --------------------------------------

    void Start()
    {
        spawnCounter = timeToSpawn;
        oldEnemySpawnCounter = oldEnemySpawnRate; // Inicializa o contador de inimigos antigos

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            target = player.GetComponent<Transform>();
        }

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 7f;

        currentWave--;
        GoToNextWave();
    }

    void Update()
    {
        if (PlayerHealth.instance != null && PlayerHealth.instance.gameObject.activeSelf)
        {
            if (currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if (waveCounter <= 0)
                {
                    GoToNextWave();
                }

                // Inimigo da Wave Atual
                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    if (spawnedEnemies.Count < MAX_ENEMIES)
                    {
                        SpawnEnemy(waves[currentWave].enemyToSpawn);
                    }
                }

                // Inimigos Antigos
                oldEnemySpawnCounter -= Time.deltaTime;
                if (oldEnemySpawnCounter <= 0)
                {
                    oldEnemySpawnCounter = oldEnemySpawnRate;

                    if (spawnedEnemies.Count < MAX_ENEMIES)
                    {
                        TrySpawnOldEnemy();
                    }
                }
            }
        }

        // Move o spawner para a posição do Player
        if (target != null)
        {
            transform.position = target.position;
        }

        // Destrói inimigos que estão muito longe.
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

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, SelectSpawnPoint(), Quaternion.identity);
        spawnedEnemies.Add(newEnemy);
    }

    // FUNÇÃO DE SPAWN ANTIGO
    private void TrySpawnOldEnemy()
    {
        // Só tenta spawnar se houver waves anteriores
        if (currentWave <= 0)
        {
            return;
        }

        // chance para aparecer em menor número
        if (Random.value < oldEnemySpawnChance)
        {
            // Escolhe aleatoriamente uma wave entre 0 e (currentWave - 1)
            int randWaveIndex = Random.Range(0, currentWave);
            GameObject oldEnemyPrefab = waves[randWaveIndex].enemyToSpawn;

            SpawnEnemy(oldEnemyPrefab);
        }
    }
    // ------------------------------

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

// --- CLASSE PARA CONFIGURAÇÃO DE WAVES ---
[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}