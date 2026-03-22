using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 12f;
    public float damage = 10f;
    private float hitCooldown = 0.5f;
    private float hitTimer = 0f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        hitTimer -= Time.deltaTime;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hitTimer <= 0f)
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            hitTimer = hitCooldown;
        }
    }
}