using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public float currentHealth, maxHealth;

    public Slider healthSlider;

    private Animator anim;
    private bool dead = false;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        // // Barra de vida
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        currentHealth -= damage;

        if (healthSlider != null)
        {
            // Atualiza a barra de vida
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true;

            // Chama a animação de morte
            if (anim != null)
            {
                anim.SetTrigger("isDead");
            }

            // Para o movimento do jogador
            GetComponent<PlayerMovement1>().enabled = false;
        }
    }

    public void FreezeGame()
    {
        // Trava a animação no último frame
        if (anim != null)
        {
            anim.enabled = false;
        }

        // Para o jogo
        Time.timeScale = 0f;
    }
}