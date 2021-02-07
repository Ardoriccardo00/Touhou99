using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Vector3 moveDirection;
    //public GameObject impactEffect;
    private Transform playerPosition;
    [Obsolete]
    Player player;
    [Obsolete]
    Player theClosestPlayer;

    [Obsolete]
    void Start()
    {
        FindClosestPlayer();
        MoveEnemyBullet();
    }

    [Obsolete]
    private void MoveEnemyBullet()
    {
        moveDirection = (theClosestPlayer.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    [Obsolete]
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Arena")
        {
            Destroy(gameObject);
        }
    }

    [Obsolete]
    void FindClosestPlayer()
    {
        float distanceClosestPlayer = Mathf.Infinity;
        Player closestPlayer = null;
        Player[] allPlayers = GameObject.FindObjectsOfType<Player>();

        foreach (Player currentArena in allPlayers)
        {
            float distanceToPlayer = (currentArena.transform.position - this.transform.position).sqrMagnitude;

            if (distanceToPlayer < distanceClosestPlayer)
            {
                distanceClosestPlayer = distanceToPlayer;
                closestPlayer = currentArena;
                theClosestPlayer = closestPlayer;
            }
        }
    }
}