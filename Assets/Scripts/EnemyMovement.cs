using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;
    private Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = FindAnyObjectByType<PlayerMovement1>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = (target.position - transform.position).normalized * moveSpeed;
    }
}
