using UnityEngine;
using Terresquall;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb;

    public Vector2 moveInput;
    public Vector2 lastMoveDirection;

    [Header("Forças externas")]
    public Vector2 externalForce;
    public float externalForceLimit = 25f; // 👈 limite pra não ficar absurdo

    public float pickupRange = 1.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveX, moveY;

        if (VirtualJoystick.CountActiveInstances() > 0)
        {
            moveX = VirtualJoystick.GetAxis("Horizontal");
            moveY = VirtualJoystick.GetAxis("Vertical");
        }
        else
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }

        moveInput = new Vector2(moveX, moveY);

        // 👉 GUARDA A ÚLTIMA DIREÇÃO
        if (moveInput.sqrMagnitude > 0.01f)
        {
            lastMoveDirection = moveInput.normalized;
        }

        // Animator
        if (moveInput.sqrMagnitude > 0.01f)
        {
            anim.SetBool("isMoving", true);
            anim.SetFloat("moveX", moveX);
            anim.SetFloat("moveY", moveY);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    void FixedUpdate()
    {
        // Movimento do jogador
        Vector2 movement = moveInput.normalized * moveSpeed;

        // Limita força externa
        externalForce = Vector2.ClampMagnitude(externalForce, externalForceLimit);

        // Aplica movimento + forças externas
        rb.linearVelocity = movement + externalForce;

        // Zera a força externa (importante!)
        externalForce = Vector2.zero;
    }
}