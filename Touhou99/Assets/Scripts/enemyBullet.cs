using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    private Vector3 moveDirection;
    //public GameObject impactEffect;
    private Transform playerPosition;
    [Obsolete]
    playerMovement player;
    playerMovement theClosestPlayer;

    [Obsolete]
    void Start()
    {
        //playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        FindClosestPlayer();
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

    void FindClosestPlayer()
    {
        float distanceClosestPlayer = Mathf.Infinity;
        playerMovement closestPlayer = null;
        playerMovement[] allPlayers = GameObject.FindObjectsOfType<playerMovement>();

        foreach (playerMovement currentArena in allPlayers)
        {
            float distanceToPlayer = (currentArena.transform.position - this.transform.position).sqrMagnitude;

            if (distanceToPlayer < distanceClosestPlayer)
            {
                distanceClosestPlayer = distanceToPlayer;
                closestPlayer = currentArena;
                theClosestPlayer = closestPlayer;
            }
        }

        Debug.Log("player position: " + closestPlayer.transform.position + "Arena name: " + closestPlayer.transform.name);
    }
}

/*private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (player != null)
        {
            player.RpcTakeDamage(damage, "Enemy bullet");
            Destroy(gameObject);
        }
        else if (enemy != null)
        {
        }
       
        //Instantiate(impactEffect, transform.position, transform.rotation);

    }*/
