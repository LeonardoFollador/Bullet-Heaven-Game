using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed, damage;
    private Transform target;
    private Animator anim;


    public float hitWaitTime = 0.5f;
    private float hitCounter;

    public float health = 10f;

    public float knockbackTime = 0.5f;
    public float knockBackCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement1>().transform;
        moveSpeed = Random.Range(moveSpeed * 0.8f, moveSpeed * 1.2f); // Velocidade randomica  
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.deltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 0.5f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * 2f);
            }
        }
    }

    // Update is called once per frame
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

    // Da dano enquanto o trigger do colisor estiver ativo
    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<PlayerHealth>();

        if (player)
        {
            player.TakeDamage(damage * Time.deltaTime);
        }
    }

    public void TakeDamage(float damageToTake)
    {
        if (hitCounter <= 0)
        {
            health -= damageToTake;

            if (health <= 0)
            {
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
