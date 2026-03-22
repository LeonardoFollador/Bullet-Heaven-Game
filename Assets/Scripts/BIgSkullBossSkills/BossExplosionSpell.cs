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

        // pega o tamanho ORIGINAL do sprite (sem scale)
        float spriteSize = sr.sprite.bounds.size.x;

        // calcula o scale correto
        float scale = (explosionRadius * 2) / spriteSize;

        // aplica scale
        indicator.transform.localScale = Vector3.one * scale;






        Debug.DrawLine(targetPosition, targetPosition + Vector2.right * explosionRadius, Color.blue, 10f);

        yield return new WaitForSeconds(delay);

        // 💥 explosão
        Collider2D[] hits = Physics2D.OverlapCircleAll(targetPosition, explosionRadius);

        bool damageApplied = false;

        foreach (Collider2D hit in hits)
        {
            if (damageApplied) break;

            PlayerHealth hp = hit.GetComponentInParent<PlayerHealth>();

            if (hp != null)
            {
                Vector2 playerPos = hp.transform.position;

                float distance = Vector2.Distance(targetPosition, playerPos);

                float adjustedRadius = explosionRadius * 0.8f;

                if (distance <= explosionRadius)
                {
                    Debug.Log("🔥 DANO APLICADO UMA VEZ!");

                    hp.TakeDamage(damage);

                    damageApplied = true; // 👈 impede múltiplos hits
                }
            }
        }

        Destroy(indicator);
    }
}