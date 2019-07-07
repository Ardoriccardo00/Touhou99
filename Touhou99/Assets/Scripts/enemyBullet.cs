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

    [Obsolete]
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moveDirection = (playerPosition.transform.position - transform.position).normalized * speed;
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
