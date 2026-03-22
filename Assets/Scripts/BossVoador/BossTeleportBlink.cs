using UnityEngine;
using System.Collections;

public class BossTeleportBlink : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;                // referência ao player

    [Header("Configuração do Teleport")]
    public float cooldown = 5f;             // tempo entre teleports
    public float teleportDistance = 2f;     // distância na direção do player
    public float teleportDelay = 0.3f;      // tempo que o boss fica invisível

    private float cooldownTimer;

    void Start()
    {
        if (player == null)
        {
            player = FindAnyObjectByType<PlayerMovement1>().transform;
        }
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0 && player != null)
        {
            StartCoroutine(TeleportBlinkSequence());
            cooldownTimer = cooldown;
        }
    }

    IEnumerator TeleportBlinkSequence()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        // 1️⃣ Desaparece (pisca)
        if (sr != null) sr.enabled = false;

        yield return new WaitForSeconds(teleportDelay);

        // 2️⃣ Calcula posição na direção do player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 targetPos = (Vector2)player.position + directionToPlayer * teleportDistance;

        // 3️⃣ Teleporta
        transform.position = targetPos;

        // 4️⃣ Reaparece
        if (sr != null) sr.enabled = true;
    }
}