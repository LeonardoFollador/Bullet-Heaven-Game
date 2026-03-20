using UnityEngine;
using System.Collections;

public class SwordWeapon : MonoBehaviour
{
    public float rotateSpeed;

    public Transform holder, fireballToSpawn;

    public float timeBetweenSpawn = 1f;
    private float spawnCounter;

    private float gameTime;

    [Header("Multi Spawn")]
    public float delayBetweenSwords = 0.25f;

    private void Update()
    {
        // rotação
        holder.rotation = Quaternion.Euler(0f, 0f,
            holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));

        gameTime += Time.deltaTime;

        spawnCounter -= Time.deltaTime;

        if (spawnCounter <= 0f)
        {
            int swordsToSpawn = GetSwordAmount();

            StartCoroutine(SpawnMultiple(swordsToSpawn));

            spawnCounter = timeBetweenSpawn;
        }
    }

    int GetSwordAmount()
    {
        int minutes = Mathf.FloorToInt(gameTime / 20f);
        return Mathf.Clamp(1 + minutes, 1, 5);
    }

    IEnumerator SpawnMultiple(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnSword();
            yield return new WaitForSeconds(delayBetweenSwords);
        }
    }

    void SpawnSword()
    {
        Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder)
            .gameObject.SetActive(true);
    }
}