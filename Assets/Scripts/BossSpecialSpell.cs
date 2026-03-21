using UnityEngine;

public class BossSpecialSpell : MonoBehaviour
{
    [Header("Pull Config")]
    public float pullForce = 15f;
    public float duration = 3f;
    private float timer;

    public float pullRadius = 6f;

    public bool pullToBoss = true;
    public Transform centerPoint;

    private Transform player;
    private Rigidbody2D playerRb;

    [Header("Visual")]
    public GameObject pullIndicator;
    private GameObject indicatorInstance;

    private float baseScale; // 👈 guarda o scale correto

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

        // 🔴 Instancia indicador
        indicatorInstance = Instantiate(pullIndicator, transform.position, Quaternion.identity);

        SpriteRenderer sr = indicatorInstance.GetComponent<SpriteRenderer>();

        float spriteSize = sr.bounds.size.x;
        float desiredSize = pullRadius * 2;

        baseScale = desiredSize / spriteSize;

        indicatorInstance.transform.localScale = Vector3.one * baseScale;

        timer = duration;
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

        ApplyPull();
        UpdateIndicator();
    }

    void ApplyPull()
    {
        Vector2 targetPosition;

        if (pullToBoss || centerPoint == null)
            targetPosition = transform.position;
        else
            targetPosition = centerPoint.position;

        float distance = Vector2.Distance(player.position, targetPosition);

        if (distance < pullRadius)
        {
            Vector2 direction = (targetPosition - (Vector2)player.position).normalized;

            Debug.Log(distance);

            player.GetComponent<PlayerMovement1>().externalForce += direction * pullForce * Time.deltaTime;
        }
    }

    void UpdateIndicator()
    {
        if (indicatorInstance == null) return;

        Vector3 pos;

        if (pullToBoss || centerPoint == null)
            pos = transform.position;
        else
            pos = centerPoint.position;

        indicatorInstance.transform.position = pos;

        // 🔴 efeito pulsando
        float pulse = Mathf.Sin(Time.time * 5f) * 0.02f;

        indicatorInstance.transform.localScale = Vector3.one * (baseScale + pulse);

        indicatorInstance.transform.Rotate(0, 0, 50 * Time.deltaTime);
    }
}