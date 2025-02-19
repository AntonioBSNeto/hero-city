using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 1f;
    public GameObject[] enemyPrefabs;

    public Transform[] spawnPoints;
    public bool canSpawn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        StartCoroutine(Spawner());        
    }

    private GameObject GetRandomEnemyPrefab() {
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
    }

    private Transform GetRandomSpawnPoint() {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }

    private IEnumerator Spawner() {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn) {
            Transform spawnPoint = GetRandomSpawnPoint();
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            yield return wait;
        }
    }
}
