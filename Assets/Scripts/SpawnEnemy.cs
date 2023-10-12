using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    private Player player; 
    public float spawnInterval = 5.0f;
    public float timeSinceLastSpawn = 0f;

    private void Start()
    {
        player = FindObjectOfType<Player>(); 

        if (player == null)
        {
            Debug.LogError("No se encontró el objeto del jugador en la escena.");
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnEnemies();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnEnemies()
    {
        int numberOfEnemies = player.currentLevel; 
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
    