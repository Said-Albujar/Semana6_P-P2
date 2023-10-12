using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public int health = 1;
    public float moveSpeed = 3.0f;
    private Player playerC;
    private float TimeToLevel = 0f;
    private float LevelUpTimer = 10f;
    private bool CanLevelUp;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        playerC = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("No se encontró el objeto del jugador en la escena.");
        }

        if (playerC == null)
        {
            Debug.LogError("No se encontró el objeto del jugador en la escena.");
        }
    }

    private void Update()
    {
        if (health >= 0)
        {
            if (CanLevelUp)
            {
                SpeedLevelUp();
            }
            else
            {
                TimeToLevel -= Time.deltaTime;
                if (TimeToLevel <= 0f)
                {
                    CanLevelUp = true;
                }
            }

            if (player != null)
            {
                Vector3 directionToPlayer = player.position - transform.position;
                directionToPlayer.Normalize();
                transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void SpeedLevelUp()
    {
        StartTimerLevelUp();
        moveSpeed += 0.5f;
    }

    void StartTimerLevelUp()
    {
        TimeToLevel = LevelUpTimer;
        CanLevelUp = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            health--;

            if (health <= 0)
            {
                playerC.KillCount += 1;
                Destroy(gameObject);
            }

            Destroy(other.gameObject);
        }
    }
}
