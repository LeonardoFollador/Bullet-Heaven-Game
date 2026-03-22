using UnityEngine;

public class BossExplosionSpell : MonoBehaviour
{
    public GameObject dangerIndicator;

    public float delay = 1.5f;
    public float cooldown = 4f; // 👈 tempo entre ataques

    public float explosionRadius = 2.5f;
    public float damage = 50f;
    public float predictionDistance = 2f;

    private Transform player;
    private Rigidbody2D playerRb;

    private float cooldownTimer;



    void Start()
    {
        PlayerMovement1 playerScript = FindAnyObjectByType<PlayerMovement1>();

        if (playerScript == null)
        {
            Debug.LogWarning("Player não encontrado!");
            return;
        }

        player = playerScript.transform;
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            StartCoroutine(CastExplosion());
            cooldownTimer = cooldown;
        }
    }

    System.Collections.IEnumerator CastExplosion()
    {
        // 🔮 previsão
        Vector2 velocity = playerRb.linearVelocity;

        Vector2 targetPosition;

        if (velocity.magnitude < 0.1f)
        {
            targetPosition = player.position;
        }
        else
        {
            targetPosition = (Vector2)player.position + velocity.normalized * predictionDistance;
        }

        // 🔴 indicador
        GameObject indicator = Instantiate(dangerIndicator, targetPosition, Quaternion.identity);

        SpriteRenderer sr = indicator.GetComponent<SpriteRenderer>();

        float spriteSize = sr.sprite.bounds.size.x;
        float finalScale = (explosionRadius * 2) / spriteSize;

        indicator.transform.localScale = Vector3.zero;

        float timer = 0f;

        // 📌 CONFIGURAÇÕES NOVAS
        float shakeStartTime = delay * 0.7f; // começa a tremer nos últimos 30%
        float shakeIntensity = 0.1f;

        Vector3 originalPos = indicator.transform.position;

        while (timer < delay)
        {
            timer += Time.deltaTime;

            float t = timer / delay;

            // crescimento
            float currentScale = Mathf.Lerp(0f, finalScale, t * t);
            indicator.transform.localScale = Vector3.one * currentScale;

            // 🎯 TREMIDA
            if (timer >= shakeStartTime)
            {
                float shakeX = Random.Range(-1f, 1f) * shakeIntensity;
                float shakeY = Random.Range(-1f, 1f) * shakeIntensity;

                indicator.transform.position = originalPos + new Vector3(shakeX, shakeY, 0);
            }
            else
            {
                indicator.transform.position = originalPos;
            }

            yield return null;
        }

        // ⚫ MUDA cor NA HORA DA EXPLOSÃO
        sr.color = Color.red;

        // 💥 pequena pausa pra dar impacto visual (opcional)
        yield return new WaitForSeconds(0.1f);

        // 💥 explosão
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPosition, explosionRadius);

        bool damageApplied = false;

        foreach (Collider2D hit in hits)
        {
            if (damageApplied) break;

            PlayerHealth hp = hit.GetComponentInParent<PlayerHealth>();

            if (hp != null)
            {
                float distance = Vector2.Distance(targetPosition, hp.transform.position);

                if (distance <= explosionRadius)
                {
                    Debug.Log("🔥 DANO APLICADO ExplosionSkill!");

                    hp.TakeDamage(damage);
                    damageApplied = true;
                }
            }
        }

        Destroy(indicator);
    }
}