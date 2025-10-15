using UnityEngine;
using Terresquall;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed;
    private Animator anim;
    private Rigidbody2D rb; // usar Rigidbody2D para movimento 2D

    public Vector2 moveInput;

    public float pickupRange = 1.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // Pega o componente Rigidbody2D
    }

    void Update()
    {
        float moveX, moveY;
        // Pega o input do jogador
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

        // Atualiza o Animator
        if (moveInput.sqrMagnitude > 0.01f) // Se o jogador est� se movendo
        {
            anim.SetBool("isMoving", true);

            // Atualiza os par�metros de dire��o. A Blend Tree de ANDAR vai usar isso.
            anim.SetFloat("moveX", moveX);
            anim.SetFloat("moveY", moveY);
        }
        else // Se o jogador est� PARADO
        {
            anim.SetBool("isMoving", false);
            // O Animator vai parar de receber atualiza��es de dire��o aqui.
            // Ele vai continuar usando os �LTIMOS VALORES de moveX e moveY que recebeu,
            // fazendo a Blend Tree de IDLE mostrar a dire��o correta.
        }
    }

    // FixedUpdate para aplicar f�sica (movimento)
    void FixedUpdate()
    {
        // Aplica o movimento
        rb.linearVelocity = moveInput.normalized * moveSpeed;
    }
}