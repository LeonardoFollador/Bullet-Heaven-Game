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

    private HashSet<Vector3> spawnedPositions = new HashSet<Vector3>();

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOptDist; // deve ser maior que a largura e altura do tilemap 
    float optDist;

    void Start()
    {
        pm = FindFirstObjectByType<PlayerMovement1>();
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk) return;

        Vector2 input = pm.moveInput;
        float deadzone = 0.25f;
        Vector3 checkPos = Vector3.zero;

        float absX = Mathf.Abs(input.x);
        float absY = Mathf.Abs(input.y);

        if (absX < deadzone && absY < deadzone)
        {
            return;
        }

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
        spawnedChunks.Add(newChunk);
    }

    void ChunkOptimizer()
    {
        foreach (GameObject chunk in spawnedChunks)
        {
            optDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (optDist > maxOptDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
