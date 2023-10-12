using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public float moveSpeed;
    public float rotationSpeed;
    public bool CanShoot;

    public int health;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private CharacterController characterController;
    private Vector3 moveDirection;

    public float nextFireTime = 0;
    public float autoFireInterval;

    [Header("Level Up")]
    public int currentLevel = 1;
    public float TimeToLevel = 0f;
    public float LevelUpTimer = 10f;
    private bool CanLevelUp;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        characterController = GetComponent <CharacterController>();

        StartTimerShoot();
        StartTimerLevelUp();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * verticalInput + transform.right * horizontalInput;
        moveDirection = move.normalized;

        if (moveDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }

        characterController.SimpleMove(moveDirection * moveSpeed);

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
            if(TimeToLevel <= 0f)
            {
                CanLevelUp = true;
            }
        }
    }

    void StartTimerShoot()
    {
        nextFireTime = autoFireInterval;
        CanShoot = false;
    }

    void StartTimerLevelUp()
    {
        TimeToLevel = LevelUpTimer;
        CanLevelUp = false;
    }
    void Shoot()
    {
        StartTimerShoot();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent <Rigidbody>();

        rb.AddForce(firePoint.forward * 10f, ForceMode.Impulse);
    }

    void LevelUp()
    {
        StartTimerLevelUp();
        autoFireInterval -= 0.5f;
    }
}
