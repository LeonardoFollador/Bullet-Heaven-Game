using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;

    [Header("Config")]
    public float moveSpeed = 9f; // 👈 velocidade fixa
    public float damage;

    public float health = 10f;

    private float damageCooldownTimer = 0f;
  

    public bool isBoss;

    [Header("Knockback")]
    public float knockbackTime = 0.5f;
    private float knockBackCounter;

    [Header("Ataque")]
    public float damageCooldown = 0.5f;
    private float damageTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement1>().transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }

        if (damageCooldownTimer > 0)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;

        // 👉 se estiver em knockback, anda ao contrário
        if (knockBackCounter > 0)
        {
            rb.linearVelocity = -direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = direction * moveSpeed;
        }

        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerHealth>();

        if (player && damageTimer <= 0)
        {
            player.TakeDamage(damage);
            damageTimer = damageCooldown;
        }
    }

    public void TakeDamage(float damageToTake)
    {
        if (damageCooldownTimer > 0) return; // 👈 trava o spam

        damageCooldownTimer = damageCooldown;

        health -= damageToTake;

        if (health <= 0)
        {
            if (isBoss) Debug.Log("Boss derrotado!");

            Destroy(gameObject);
            ScoreController.updateScore(gameObject.name);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }

  

    public void TakeDamage(float damageToTake, bool shouldKnockback)
    {
        TakeDamage(damageToTake);

        if (shouldKnockback)
        {
            knockBackCounter = knockbackTime;
        }
    }
}