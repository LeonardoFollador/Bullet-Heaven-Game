using UnityEngine;
using System.Collections.Generic;

public class Proprandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoint;
    public List<GameObject> propPrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoint)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
