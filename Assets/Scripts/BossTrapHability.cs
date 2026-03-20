using UnityEngine;

public class BossTrapAbility : MonoBehaviour
{
    public GameObject wallPrefab;
    public int wallCount = 12;
    public float radius = 3f;

    public float cooldown = 8f;
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
            CreateTrap();
            timer = cooldown;
        }
    }

    void CreateTrap()
    {
        for (int i = 0; i < wallCount; i++)
        {
            float angle = i * Mathf.PI * 2 / wallCount;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            Vector2 spawnPosition = (Vector2)player.position + new Vector2(x, y);

            GameObject wall = Instantiate(wallPrefab, spawnPosition, Quaternion.identity);

            Destroy(wall, 30f); // destrói depois de 30 segundos
        }
    }
}

