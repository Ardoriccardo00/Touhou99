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
    public Transform playerPosition;
    [Obsolete]
    playerMovement player;
    private int timeToSurvive = 150;

    [Obsolete]
    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        if (player != null) { moveDirection = (player.transform.position - transform.position).normalized * speed; }
        //try{moveDirection = (player.transform.position - transform.position).normalized * speed;}
        //catch(NullReferenceException) { throw new NullReferenceException("Nemico non trovato"); }

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    void Update()
    {
        timeToSurvive--;
        if (timeToSurvive <= 0) { Destroy(gameObject); }
    }

    [Obsolete]
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (hitInfo.tag == "Player")
        {
            player.RpcTakeDamage(damage, "Enemy bullet");
            Destroy(gameObject);
            //Instantiate(impactEffect, transform.position, transform.rotation);
        }

        else if (hitInfo.tag == "Arena")
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
