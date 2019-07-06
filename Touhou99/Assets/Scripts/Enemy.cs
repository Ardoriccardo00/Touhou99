using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Enemy : NetworkBehaviour
{
    //Altre variabili
    public GameObject deathEffect;
    private Rigidbody2D rb;
    public float moveSpeed = 50f;
    playerMovement player;

    private string direction;

    private Vector3 spawnPosition;

    public int currentHealth;
    private int health = 1;

    float movement = 0f;

    Vector2 moveDirection;
    Vector2 moveDirection2;

    float timer = 1f;
    float timer2 = 3f;

    void Start()
    {
        spawnPosition = this.transform.localPosition;
        Debug.Log("posizione spawn" + spawnPosition);
        currentHealth = health;
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4f);
        GetPosition();
        GetMoveDirection();
    }

    void Update()
    {
        if (currentHealth <= 0)
            NetworkServer.Destroy(gameObject);

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

            if(timer2 <= 0 && timer <= 0)
            {
                timer = 1f;
                timer2 = 3f;
            }
        }    
    }

    void GetPosition()
    {
        if (spawnPosition == new Vector3(-1, 4.5f, 0))
            direction = "up center left";

        else if(spawnPosition == new Vector3(1, 4.5f, 0))
            direction = "up center right";

        else if (spawnPosition == new Vector3(-3.5f, 5.555556f, 0))
            direction = "up left";

        else if (spawnPosition == new Vector3(3.5f, 5.555556f, 0))
            direction = "up right";
    }

    void GetMoveDirection()
    {
        switch (direction)
        {
            case "null":
                break;
            case "up center left":
                //moveDirection = new Vector2(0f, -1f * moveSpeed);
                //moveDirection2 = new Vector2(1f * moveSpeed, 0f);
                break;
            case "up center right":
                //moveDirection = new Vector2(0f, -1f * moveSpeed);
                //moveDirection2 = new Vector2(-1f * moveSpeed, 0f);
                break;

            case "up left":
                moveDirection = new Vector2(0f, -1f * moveSpeed);
                moveDirection2 = new Vector2(1f * moveSpeed, 0f);
                break;

            case "up right":
                moveDirection = new Vector2(0f, -1f * moveSpeed);
                moveDirection2 = new Vector2(-1f * moveSpeed, 0f);
                break;


        }
    }

    //private void OnTriggerEnter2D(Collider2D hitInfo)
    //{
    //    if (hitInfo.tag == "Player")
    //    {
    //        Destroy(gameObject);
    //    }

    //}

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

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class Enemy : NetworkBehaviour
{
//Altre variabili
public GameObject deathEffect;
private Rigidbody2D rb;
public float moveSpeed = 50f;
playerMovement player;

public string direction;


public int currentHealth;
private int health = 1;

float movement = 0f;

Vector3 spawnPostion;
Vector3 directionToGo;

void Start()
{
    currentHealth = health;
    rb = GetComponent<Rigidbody2D>();
    Destroy(gameObject, 4f);
    spawnPostion = this.transform.position;
}

void Update()
{
    GetPosition();
    //MoveDirection();
    if (currentHealth <= 0)
        NetworkServer.Destroy(gameObject);
}
private void FixedUpdate()
{

}
private void GetPosition()
{
    //rb.velocity = transform.up * moveSpeed;
    //rb.velocity = Vector2.zero; 

    if (spawnPostion == new Vector3(-1, 4.5f, 0))
    {
        //GetDirection("up center left");

    }
    else if (spawnPostion == new Vector3(1, 4.5f, 0))
    {
        GetDirection("up center right");
    }
    else
    {
        //direction = "zero";
    }
}

private void GetDirection(string _direction)
{
    switch (_direction)
    {
        case "up center left":
            float moveTimer = 1f;
            if(moveTimer > 0f)
            {
                moveTimer -= Time.deltaTime;
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
            }
            else if(moveTimer <= 0f)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
            }

            break;
    }
}


//private void OnTriggerEnter2D(Collider2D hitInfo)
//{
//    if (hitInfo.tag == "Player")
//    {
//        Destroy(gameObject);
//    }

//}

}
*/