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
    private Rigidbody2D rb;
    public int moveSpeed = 5;
    playerMovement player;

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

    //public EnemySpawner spawner;


    void Start()
    {
        //spawner = FindObjectOfType<EnemySpawner>();

        rb = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove; 
    }

    void Update()
    {
        timeToMoveCounter -= Time.deltaTime;
        GetUpdate();
    }

    private void GetUpdate()
    {
        rb.velocity = Vector2.zero;     
        switch (Direction)
        {
            case "right":
                rb.velocity = new Vector2(1f * moveSpeed, rb.velocity.y);
                break;
            case "left":
                rb.velocity = new Vector2(-1f * moveSpeed, rb.velocity.y);
                break;
            case "up":
                rb.velocity = new Vector2(rb.velocity.x, 1f * moveSpeed);
                break;
            case "down":
                rb.velocity = new Vector2(rb.velocity.x, -1f * moveSpeed);
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
        Destroy(gameObject);
        //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag != "Enemy")
        {
            Destroy(gameObject);
        }
            
    }

}

/*    //Altre variabili
    public int health = 100;
    public GameObject deathEffect;
    private Rigidbody2D rb;
    public int speed = 5;
    playerMovement player;

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

    //public EnemySpawner spawner;


    void Start()
    {
        //spawner = FindObjectOfType<EnemySpawner>();

        rb = GetComponent<Rigidbody2D>();
        timeBetweenMoveCounter = timeBetweenMove;
        timeToMoveCounter = timeToMove; 
    }

    void Update()
    {
        timeToMoveCounter -= Time.deltaTime;
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
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
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
        Destroy(gameObject);
        //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
    }*/
