using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed, damage;
    private float baseMoveSpeed;
    private Transform target;
    private Animator anim;

    public float hitWaitTime = 0.5f;
    private float hitCounter;

    public float health = 10f;

    public float knockbackTime = 0.5f;
    public float knockBackCounter;

    public bool isBoss;

    [Header("Aceleração por Tempo")]
    public float maxMoveSpeed = 15f;
    public float accelerationRate = 100.0f;

    private float timeToUpdateMoveSpeed = 0;

    private const float TIME_TO_UPDATE_MOVE_SPEED = 0.2f;

    [Header("Configuração de Ataque")]
    public int hitsToReset = 1;
    private int hitsGivenCounter;
    private float damageCooldown = 0.5f;
    private float damageTimer;

    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement1>().transform;

        if (!isBoss)
        {
            moveSpeed = Random.Range(moveSpeed * 0.8f, moveSpeed * 1.2f);
        }

        baseMoveSpeed = moveSpeed;
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        timeToUpdateMoveSpeed += Time.deltaTime;

        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;
            if (moveSpeed > 0) moveSpeed = -moveSpeed * 0.5f;
            if (knockBackCounter <= 0) moveSpeed = Mathf.Abs(moveSpeed * 2f);
        }

        if (hitCounter <= 0 && knockBackCounter <= 0)
        {
            if (timeToUpdateMoveSpeed > TIME_TO_UPDATE_MOVE_SPEED){
                moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, accelerationRate * Time.deltaTime);
                timeToUpdateMoveSpeed = 0;
            }
        }

        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerHealth>();
        if (player && damageTimer <= 0)
        {
            player.TakeDamage(damage);
            damageTimer = damageCooldown;
            hitsGivenCounter++;

            if (hitsGivenCounter >= hitsToReset)
            {
                moveSpeed = baseMoveSpeed;
                hitCounter = hitWaitTime;
                hitsGivenCounter = 0;
            }
        }
    }

    public void TakeDamage(float damageToTake)
    {
        if (hitCounter <= 0)
        {
            health -= damageToTake;
            moveSpeed = baseMoveSpeed;
            hitsGivenCounter = 0;

            if (health <= 0)
            {
                if (isBoss) Debug.Log("Boss derrotado!");
                Destroy(gameObject);
                ScoreController.updateScore(gameObject.name);
            }

            DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
            hitCounter = hitWaitTime;
        }
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