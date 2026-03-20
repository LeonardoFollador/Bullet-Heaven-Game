using UnityEngine;

public class BossSpecialAbility : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int enemyCount = 8;
    public float radius = 3f;

    public float cooldown = 5f;
    private float timer;

    private Transform player;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement1>().transform;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SummonCircle();
            timer = cooldown;
        }
    }

    void SummonCircle()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            float angle = i * Mathf.PI * 2 / enemyCount;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector2 spawnPosition = (Vector2)player.position + new Vector2(x, y);

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}