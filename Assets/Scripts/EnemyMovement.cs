using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private Transform target;
    private Animator anim;

    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement1>().transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed;

        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }
}
