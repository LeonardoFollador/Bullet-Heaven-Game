using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed, damage;
    private Transform target;

    public float hitWaitTime = 0.5f;
    private float hitCounter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement1>().transform;
        moveSpeed = Random.Range(moveSpeed * 0.8f, moveSpeed* 1.2f); // Velocidade randomica  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity = (target.position - transform.position).normalized * moveSpeed;

        if (hitCounter > 0)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        var player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player)
        {
            player.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }
}
