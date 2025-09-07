using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb; // usar Rigidbody2D para movimento 2D

    private Vector2 moveInput;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Pega o componente Rigidbody2D
    }

    void Update()
    {
        // Pega o input do jogador
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY);

        // Atualiza o Animator
        if (moveInput.sqrMagnitude > 0.01f) // Se o jogador está se movendo
        {
            anim.SetBool("isMoving", true);

            // Atualiza os parâmetros de direção. A Blend Tree de ANDAR vai usar isso.
            anim.SetFloat("moveX", moveX);
            anim.SetFloat("moveY", moveY);
        }
        else // Se o jogador está PARADO
        {
            anim.SetBool("isMoving", false);
            // O Animator vai parar de receber atualizações de direção aqui.
            // Ele vai continuar usando os ÚLTIMOS VALORES de moveX e moveY que recebeu,
            // fazendo a Blend Tree de IDLE mostrar a direção correta.
        }
    }

    // FixedUpdate para aplicar física (movimento)
    void FixedUpdate()
    {
        // Aplica o movimento
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}