using UnityEngine;

public class BossSpecialSpell2 : MonoBehaviour
{
    [Header("Spawn Config")]
    public float spawnDistance = 2f;

    [Header("Pull Config")]
    public float pullForce = 12f;
    public float duration = 3f;
    private float timer;
    public float pullRadius = 5f;

    [Header("Damage")]
    public float damagePerSecond = 5f;

    private Transform player;
    private Rigidbody2D playerRb;

    private Vector2 spawnPosition;

    [Header("Visual")]
    public GameObject pullIndicator;
    private GameObject indicatorInstance;
    private float baseScale;

    [Header("Scale Animation")]
    public float growTime = 1.2f;
    private float growTimer = 0f;
    private bool isFullyGrown = false;
    private float currentScale = 0f;

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

        // 👉 PEGA DIREÇÃO DO PLAYER
        Vector2 moveDir = playerScript.lastMoveDirection;

        if (moveDir == Vector2.zero)
        {
            // fallback caso esteja parado
            moveDir = Vector2.up;
        }

        // 👉 DEFINE POSIÇÃO NA FRENTE DO PLAYER
        spawnPosition = player.position + (Vector3)(moveDir.normalized * spawnDistance);
        transform.position = spawnPosition;

        // 🔴 indicador
        indicatorInstance = Instantiate(pullIndicator, spawnPosition, Quaternion.identity);

        SpriteRenderer sr = indicatorInstance.GetComponent<SpriteRenderer>();

        float spriteSize = sr.bounds.size.x;
        float desiredSize = pullRadius * 2;

        baseScale = desiredSize / spriteSize;
        indicatorInstance.transform.localScale = Vector3.zero;

        timer = duration;
    }

    void GrowEffect()
    {
        if (indicatorInstance == null) return;

        if (!isFullyGrown)
        {
            growTimer += Time.deltaTime;

            float t = growTimer / growTime;

            float scale = Mathf.Lerp(0f, baseScale, t);
            currentScale = scale;

            indicatorInstance.transform.localScale = Vector3.one * currentScale;

            if (t >= 1f)
            {
                isFullyGrown = true;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (indicatorInstance != null)
                Destroy(indicatorInstance);

            Destroy(this);
            return;
        }

        GrowEffect(); // 👈 primeiro cresce

        if (isFullyGrown)
        {
            ApplyPullAndDamage(); // 👈 depois ativa
        }

        UpdateIndicator();
    }

    void ApplyPullAndDamage()
    {
        float distance = Vector2.Distance(player.position, spawnPosition);

        if (distance < pullRadius)
        {
            Vector2 direction = (spawnPosition - (Vector2)player.position).normalized;

            // 🌀 PUXA
            player.GetComponent<PlayerMovement1>().externalForce += direction * pullForce * Time.deltaTime;

            // ❤️ DANO AO LONGO DO TEMPO
            PlayerHealth health = player.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    void UpdateIndicator()
    {
        if (indicatorInstance == null) return;

        indicatorInstance.transform.position = spawnPosition;

        float pulse = Mathf.Sin(Time.time * 5f) * 0.02f;
        indicatorInstance.transform.localScale = Vector3.one * (currentScale + pulse);

        indicatorInstance.transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    void OnDestroy()
    {
        if (indicatorInstance != null)
        {
            Destroy(indicatorInstance);
        }
    }
}