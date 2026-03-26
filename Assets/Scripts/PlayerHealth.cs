using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameOverController gameOverController;
    public static PlayerHealth instance;
    public float currentHealth, maxHealth;
    public Slider healthSlider;
    private Animator anim;
    private bool dead = false;

    private float cureCounter = 0;
    public float timeToCure = 5f;
    public float healthRegenAmount = 10f;

    void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }

    void Update()
    {
        if (dead) return;

        cureCounter += Time.deltaTime;

        if (cureCounter > timeToCure)
        {
            cureCounter = 0;

            currentHealth += healthRegenAmount;

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;

        currentHealth -= damage;
        cureCounter = 0;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true;

            if (anim != null)
            {
                anim.SetTrigger("isDead");
            }

            GetComponent<PlayerMovement1>().enabled = false;
            gameOverController.ShowGameOverUI();
        }
    }

    public void FreezeGame()
    {
        if (anim != null)
        {
            anim.enabled = false;
        }
        Time.timeScale = 0f;
    }
}