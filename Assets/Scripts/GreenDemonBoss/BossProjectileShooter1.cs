using UnityEngine;

public class BossProjectileShooter1 : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float firstShotDelay = 3f;
    public float cooldown = 4f;
    private float hitCooldown = 0.5f;
    private float hitTimer = 0f;

    private float timer;

    Vector2 RotateVector(Vector2 v, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos
        );
    }

    void ShootProjectile(Vector2 direction)
    {
        Vector3 spawnPos = transform.position + (Vector3)(direction * 1.5f);

        GameObject proj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);

        BossProjectile projectile = proj.GetComponent<BossProjectile>();

        if (projectile != null)
        {
            projectile.SetDirection(direction);
        }
    }

    void Start()
    {
        timer = firstShotDelay;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        hitTimer -= Time.deltaTime;

        if (timer <= 0)
        {
            Shoot();
            timer = cooldown;
        }
    }

    void Shoot()
    {
        PlayerMovement1 player = FindAnyObjectByType<PlayerMovement1>();
        if (player == null) return;

        Vector2 direction = (player.transform.position - transform.position).normalized;

        ShootProjectile(direction);
    }
}