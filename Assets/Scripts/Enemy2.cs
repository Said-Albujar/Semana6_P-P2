using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Enemy2 : MonoBehaviour
{
    public Transform player;
    private Player playerC;
    public GameObject bulletPrefab;
    public Transform firePoint;
    private int health = 1;
    public float fireRate = 2.0f;
    public float nextFireTime;
    public bool CanShoot;
    private float TimeToLevel = 0f;
    private float LevelUpTimer = 10f;
    private bool CanLevelUp;

    private void Start()
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
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = lookRotation;

            if (CanShoot)
            {
                Shoot();
            }
            else
            {
                nextFireTime -= Time.deltaTime;

                if (nextFireTime <= 0f)
                {
                    CanShoot = true;
                }
            }

            if (CanLevelUp)
            {
                LevelUp();
            }
            else
            {
                TimeToLevel -= Time.deltaTime;
                if (TimeToLevel <= 0f)
                {
                    CanLevelUp = true;
                }
            }
        }
    }

    void StartTimerLevelUp()
    {
        TimeToLevel = LevelUpTimer;
        CanLevelUp = false;
    }

    void StartTimerShoot()
    {
        nextFireTime = fireRate;
        CanShoot = false;
    }
    void LevelUp()
    {
        StartTimerLevelUp();
        fireRate -= 0.5f;
    }

    private void Shoot()
    {
        StartTimerShoot();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * 10f, ForceMode.Impulse);
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
