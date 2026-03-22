using UnityEngine;

public class BossVoadorSpawnEffect : MonoBehaviour
{
    public float darkness = 0.6f;
    public float cooldown = 5f; // tempo mínimo entre escurecimentos
    private float timer = 0f;

    private ScreenDarknerForASecond darkener;

    void Start()
    {
        darkener = FindAnyObjectByType<ScreenDarknerForASecond>();
        if (darkener == null)
            Debug.LogWarning("ScreenDarknerForASecond não encontrado!");
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }

        // Aqui você pode colocar a condição da skill
        // Por exemplo: se o boss estiver atacando ou usando a skill
        if (BossUsandoSkill() && timer <= 0f && darkener != null)
        {
            darkener.Darken(darkness);
            timer = cooldown; // reinicia cooldown
        }
    }

    // Substitua essa função pela sua condição real de skill do boss
    private bool BossUsandoSkill()
    {
        // Retorne true quando o boss estiver usando a skill
        // Exemplo:
        // return bossScript.skillAtiva;
        return true; // para teste
    }
}