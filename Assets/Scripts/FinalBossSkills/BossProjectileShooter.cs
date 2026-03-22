using UnityEngine;

public class BossProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float firstShotDelay = 3f;
    public float cooldown = 4f;
    private float hitCooldown = 0.5f;
    private float hitTimer = 0f;

    private float timer;

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
        Debug.Log("ATIROU");

        PlayerMovement1 player = FindAnyObjectByType<PlayerMovement1>();

        if (player == null) return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector2 direction = player.transform.position - transform.position;

        BossProjectile projectile = proj.GetComponent<BossProjectile>();

        if (projectile != null)
        {
            projectile.SetDirection(direction);
        }
    }
}