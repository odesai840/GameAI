using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> policePrefabs;
    [Tooltip("Time to wait until spawner starts spawning police")]
    [SerializeField] private float spawnerStartTime = 5f;
    [Tooltip("Time to wait between police spawns")]
    [SerializeField] private float spawnCooldown = 10f;

    void Start()
    {
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(spawnerStartTime);
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        StopCoroutine(WaitToSpawn());
        int index = Random.Range(0, policePrefabs.Count);
        Instantiate(policePrefabs[index], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnCooldown);
        StartCoroutine(SpawnRoutine());
    }
}
