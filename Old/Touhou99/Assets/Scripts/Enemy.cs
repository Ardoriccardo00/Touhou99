using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Enemy : NetworkBehaviour
{
    [Header("Stats")]
    public GameObject deathEffect;
    private Rigidbody2D rb;
    public float moveSpeed = 50f;
    public int currentHealth;
    private int health = 1;

    [Header("Movement")]
    Vector2 moveDirection;
    Vector2 moveDirection2;

    float timer = 1f;
    float timer2 = 3f;

    public enum SpawnPositionEnum {UpLeft, UpRight};
    public SpawnPositionEnum spawnPosition;

    void Start()
    {
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
        GetPosition();
    }

    void Update()
    {
        CheckEnemyHealth();
        MoveEnemy();
    }

    private void CheckEnemyHealth()
    {
        if (currentHealth <= 0)
        {
            NetworkServer.Destroy(gameObject);
        }
    }

    private void MoveEnemy()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            rb.velocity = moveDirection;
        }
        else if (timer <= 0)
        {
            if (timer2 > 0)
            {
                timer2 -= Time.deltaTime;
                rb.velocity = moveDirection2;
            }

            if (timer2 <= 0 && timer <= 0)
            {
                timer = 1f;
                timer2 = 3f;
            }
        }
    }

    void GetPosition()
    {
        switch (spawnPosition)
        {
            case SpawnPositionEnum.UpLeft:
                moveDirection = new Vector2(0f, -1f * moveSpeed);
                moveDirection2 = new Vector2(1f * moveSpeed, 0f);
                break;

            case SpawnPositionEnum.UpRight:
                moveDirection = new Vector2(0f, -1f * moveSpeed);
                moveDirection2 = new Vector2(-1f * moveSpeed, 0f);
                break;
        }
    }
}