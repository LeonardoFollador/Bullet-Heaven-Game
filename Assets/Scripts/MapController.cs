using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement1 pm;

    // NOVO: registrar posições já usadas
    private HashSet<Vector3> spawnedPositions = new HashSet<Vector3>();

    void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement1>();
    }

    void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker()
    {
        if (!currentChunk) return;

        Vector2 input = pm.moveInput; // supondo que seja Vector2
        float deadzone = 0.25f; // ajuste conforme necessário
        Vector3 checkPos = Vector3.zero;

        float absX = Mathf.Abs(input.x);
        float absY = Mathf.Abs(input.y);

        // Decide direção principal com deadzone
        if (absX < deadzone && absY < deadzone)
        {
            // sem movimento significativo
            return;
        }

        // Se eixo X dominante -> direita ou esquerda
        if (absX > absY)
        {
            if (input.x > 0)
                checkPos = currentChunk.transform.Find("Right").position;
            else
                checkPos = currentChunk.transform.Find("Left").position;
        }
        // Se eixo Y dominante -> cima ou baixo
        else if (absY > absX)
        {
            if (input.y > 0)
                checkPos = currentChunk.transform.Find("Up").position;
            else
                checkPos = currentChunk.transform.Find("Down").position;
        }
        // Se praticamente iguais e ambos além do deadzone -> diagonais
        else
        {
            if (input.x > 0 && input.y > 0)
                checkPos = currentChunk.transform.Find("Right Up").position;
            else if (input.x > 0 && input.y < 0)
                checkPos = currentChunk.transform.Find("Right Down").position;
            else if (input.x < 0 && input.y > 0)
                checkPos = currentChunk.transform.Find("Left Up").position;
            else if (input.x < 0 && input.y < 0)
                checkPos = currentChunk.transform.Find("Left Down").position;
        }

        // Verifica existência e evita spawn duplicado
        if (checkPos != Vector3.zero)
        {
            if (!Physics2D.OverlapCircle(checkPos, checkerRadius, terrainMask)
                && !spawnedPositions.Contains(checkPos))
            {
                noTerrainPosition = checkPos;
                SpawnChunk();
            }
        }

        {
            if (!currentChunk)
                return;

            Vector3 direction = Vector3.zero;

            if (pm.moveInput.x > 0 && pm.moveInput.y == 0)
                direction = currentChunk.transform.Find("Right").position;
            else if (pm.moveInput.x < 0 && pm.moveInput.y == 0)
                direction = currentChunk.transform.Find("Left").position;
            else if (pm.moveInput.x == 0 && pm.moveInput.y > 0)
                direction = currentChunk.transform.Find("Up").position;
            else if (pm.moveInput.x == 0 && pm.moveInput.y < 0)
                direction = currentChunk.transform.Find("Down").position;
            else if (pm.moveInput.x > 0 && pm.moveInput.y > 0)
                direction = currentChunk.transform.Find("Right Up").position;
            else if (pm.moveInput.x > 0 && pm.moveInput.y < 0)
                direction = currentChunk.transform.Find("Right Down").position;
            else if (pm.moveInput.x < 0 && pm.moveInput.y > 0)
                direction = currentChunk.transform.Find("Left Up").position;
            else if (pm.moveInput.x < 0 && pm.moveInput.y < 0)
                direction = currentChunk.transform.Find("Left Down").position;

            if (direction != Vector3.zero)
            {
                if (!Physics2D.OverlapCircle(direction, checkerRadius, terrainMask)
                    && !spawnedPositions.Contains(direction))
                {
                    noTerrainPosition = direction;
                    SpawnChunk();
                }
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        GameObject newChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedPositions.Add(noTerrainPosition);
    }
}
