using UnityEngine;
using UnityEngine.UI;

public class ScreenDarkener1 : MonoBehaviour
{
    public Image overlay;
    public float fadeSpeed = 0.2f;
    public float cooldownBeforeLighten = 2f; // tempo em segundos antes de clarear automaticamente

    private float targetAlpha = 0f;
    private float cooldownTimer = 0f;
    private bool coolingDown = false;

    void Update()
    {
        // Atualiza a transparência da tela
        Color color = overlay.color;
        color.a = Mathf.MoveTowards(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
        overlay.color = color;

        // Se estiver no cooldown, conta o tempo
        if (coolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                Lighten();
                coolingDown = false;
            }
        }
    }

    // Escurece a tela e inicia cooldown para clarear
    public void Darken(float intensity = 0.6f)
    {
        targetAlpha = intensity;
        cooldownTimer = cooldownBeforeLighten; // inicia o contador
        coolingDown = true;                     // ativa o cooldown
    }

    // Clareia a tela imediatamente
    public void Clear()
    {
        targetAlpha = 0f;
        coolingDown = false; // cancela qualquer cooldown em andamento
    }

    // Alias para clarear
    public void Lighten()
    {
        targetAlpha = 0f;
    }
}