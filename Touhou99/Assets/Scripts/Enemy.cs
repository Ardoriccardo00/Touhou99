using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Enemy : NetworkBehaviour
{
    //Altre variabili
    public int health = 100;
    public GameObject deathEffect;
    private Rigidbody2D myRigidBody;
    public int speed = 5;

    //Direzioni
    public string Direction; //Stringa per la direzione nell'editor
    private Vector2 direction; //Vettore per la direzione

    private Vector2 directionRight;
    private Vector2 directionLeft;
    private Vector2 directionUp;
    private Vector2 directionDown;

    //Timer per il movimento
    //private bool moving = true;
    public float timeBetweenMove;
    private float timeBetweenMoveCounter;
    public float timeToMove;
    private float timeToMoveCounter;


    void Start()
    {
        //direction = new Vector3(0f, 0f);

        EnemySpawner spawner = GetComponent<EnemySpawner>();

        myRigidBody = GetComponent<Rigidbody2D>();

        //directionRight = new Vector2(1f * speed, 0f);
        //directionLeft = new Vector2(-1f * speed, 0f);
        //directionUp = new Vector2(0f * speed, 1f);
        //directionDown = new Vector2(0f * speed, -1f);

        

        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove;
    }

    void Update()
    {
           timeToMoveCounter -= Time.deltaTime;
        //myRigidBody.velocity = direction;
        GetUpdate();
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void GetUpdate()
    {
        direction = Vector2.zero;

        switch (Direction)
        {
            case "right":
                direction += Vector2.right;
                break;
            case "left":
                direction += Vector2.left;
                break;
            case "up":
                direction += Vector2.up;
                break;
            case "down":
                direction += Vector2.down;
                break;
        }

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

}
