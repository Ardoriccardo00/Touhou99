using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

    public GameObject deathEffect;
    private Rigidbody2D myRigidBody;
    public int speed = 5;

    private Vector3 moveDirection;
    private Vector3 directionRight;
    private Vector3 directionLeft;
    private Vector3 directionUp;
    private Vector3 directionDown;

    private bool moving;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        directionRight = new Vector3(1f * speed, 0f);
        directionLeft = new Vector3(-1f * speed, 0f);
        directionUp = new Vector3(0f * speed, 1f);
        directionDown = new Vector3(0f * speed, -1f);

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
    }

    void Update()
    {
        myRigidBody.velocity = moveDirection;
        //moveDirection = directionRight;
       /* if (moving)
        {

        }
        else
        {
            timeBetweenMoveCounter -= Time.deltaTime;
        }*/
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
        Destroy(gameObject);
    }

    void Square()
    {

    }
}
