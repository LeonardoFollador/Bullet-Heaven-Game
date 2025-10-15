using UnityEngine;
using System.Collections.Generic;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    PlayerMovement1 pm;

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
        // Direita
        if (pm.moveInput.x > 0 && pm.moveInput.y == 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(28, 0, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(28, 0, 0);
                SpawnChunk();
            }
        }

        // Esquerda
        else if (pm.moveInput.x < 0 && pm.moveInput.y == 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-28, 0, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-28, 0, 0);
                SpawnChunk();
            }
        }

        // Cima
        else if (pm.moveInput.x == 0 && pm.moveInput.y > 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, 18, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, 18, 0);
                SpawnChunk();
            }
        }

        // Baixo
        else if (pm.moveInput.x == 0 && pm.moveInput.y < 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, -18, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, -18, 0);
                SpawnChunk();
            }
        }

        // Direita + Cima
        else if (pm.moveInput.x > 0 && pm.moveInput.y > 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(28, 18, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(28, 18, 0);
                SpawnChunk();
            }
        }

        // Direita + Baixo
        else if (pm.moveInput.x > 0 && pm.moveInput.y < 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(28, -18, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(28, -18, 0);
                SpawnChunk();
            }
        }

        // Esquerda + Cima
        else if (pm.moveInput.x < 0 && pm.moveInput.y > 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-28, 18, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-28, 18, 0);
                SpawnChunk();
            }
        }

        // Esquerda + Baixo
        else if (pm.moveInput.x < 0 && pm.moveInput.y < 0)
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-28, -18, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-28, -18, 0);
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
    }
}
