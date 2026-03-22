using UnityEngine;
using System.Collections;

public class BossPullSkill : MonoBehaviour
{
    [Header("Pull Config")]
    public float pullForce = 15f;
    public float duration = 5f;       // tempo ativo do pull
    public float cooldown = 3f;       // tempo de espera entre pulls
    public float pullRadius = 6f;

    [Header("Damage Config")]
    public float damagePerSecond = 10f;

    public bool pullToBoss = true;
    public Transform centerPoint;

    [Header("Visual")]
    public GameObject pullIndicator;

    private Transform player;
    private Rigidbody2D playerRb;
    private GameObject indicatorInstance;
    private float baseScale;

    private bool isActive = false;

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

        // 🔴 Instancia indicador mas começa invisível
        indicatorInstance = Instantiate(pullIndicator, transform.position, Quaternion.identity);
        SpriteRenderer sr = indicatorInstance.GetComponent<SpriteRenderer>();
        float spriteSize = sr.bounds.size.x;
        float desiredSize = pullRadius * 2;
        baseScale = desiredSize / spriteSize;
        indicatorInstance.transform.localScale = Vector3.one * baseScale;
        indicatorInstance.SetActive(false);

        // inicia ciclo
        StartCoroutine(PullCycle());
    }

    void Update()
    {
        if (!isActive || player == null) return;

        ApplyPull();
        ApplyDamage();
        UpdateIndicator();
    }

    void ApplyPull()
    {
        Vector2 targetPosition = (pullToBoss || centerPoint == null) ? transform.position : centerPoint.position;
        float distance = Vector2.Distance(player.position, targetPosition);

        if (distance < pullRadius)
        {
            Vector2 direction = (targetPosition - (Vector2)player.position).normalized;
            player.GetComponent<PlayerMovement1>().externalForce += direction * pullForce * Time.deltaTime;
        }
    }

    void ApplyDamage()
    {
        Vector2 targetPosition = (pullToBoss || centerPoint == null) ? transform.position : centerPoint.position;
        float distance = Vector2.Distance(player.position, targetPosition);

        if (distance < pullRadius)
        {
            // Só aplica dano se estiver dentro do raio
            PlayerHealth hp = player.GetComponent<PlayerHealth>();
            if (hp != null)
            {
                hp.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    void UpdateIndicator()
    {
        if (indicatorInstance == null) return;

        Vector3 pos = (pullToBoss || centerPoint == null) ? transform.position : centerPoint.position;
        indicatorInstance.transform.position = pos;

        // efeito pulsando
        float pulse = Mathf.Sin(Time.time * 5f) * 0.02f;
        indicatorInstance.transform.localScale = Vector3.one * (baseScale + pulse);

        indicatorInstance.transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    IEnumerator PullCycle()
    {
        while (true)
        {
            // 🔹 Ativa skill
            isActive = true;
            if (indicatorInstance != null) indicatorInstance.SetActive(true);

            yield return new WaitForSeconds(duration);

            // 🔹 Desativa skill
            isActive = false;
            if (indicatorInstance != null) indicatorInstance.SetActive(false);

            yield return new WaitForSeconds(cooldown);
        }
    }

    void OnDestroy()
    {
        if (indicatorInstance != null)
        {
            Destroy(indicatorInstance);
        }
    }
}