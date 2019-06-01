using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Obsolete]
public class playerMovement : NetworkBehaviour{

    public int health;
    private Rigidbody2D rb;
    public float bombPower;
    private float originalMoveSpeed;

    [SerializeField]
    private float moveSpeed;
    private Vector2 direction;

    private Camera myCamera; // Copy of the main camera, attached to the player

    void Start()
    {
        originalMoveSpeed = moveSpeed;
        bombPower = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void OnStartLocalPlayer()
    {
        //myCamera = Instantiate(Camera.main);
        //myCamera.transform.rotation = transform.rotation;
       // myCamera.transform.position = transform.position + new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //myCamera.transform.SetParent(transform);
    }

    void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            GetInput();
        }
    }

    private void GetInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
        }

        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 4;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = originalMoveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(100);
            TakeDamage(100);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
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

//public int health;

//[SerializeField]
//private float moveSpeed;
//private Vector2 direction;
//// Update is called once per frame
//void Update()
//{

//}

//void FixedUpdate()
//{
//    //GetInput();
//    //Move();

//    if (this.isLocalPlayer)
//    {
//        GetInput();
//        Move();
//    }
//}

//public void Move()
//{
//    transform.Translate(direction * moveSpeed * Time.deltaTime);
//}

//private void GetInput()
//{
//    direction = Vector2.zero;

//    if (Input.GetKey(KeyCode.UpArrow))
//    {
//        direction += Vector2.up;
//    }
//    if (Input.GetKey(KeyCode.LeftArrow))
//    {
//        direction += Vector2.left;
//    }
//    if (Input.GetKey(KeyCode.DownArrow))
//    {
//        direction += Vector2.down;
//    }
//    if (Input.GetKey(KeyCode.RightArrow))
//    {
//        direction += Vector2.right;
//    }
//}

//private void OnTriggerEnter2D(Collider2D hitInfo)
//{
//    Enemy enemy = hitInfo.GetComponent<Enemy>();
//    if (enemy != null)
//    {
//        enemy.TakeDamage(100);
//        TakeDamage(100);
//    }
//    //Instantiate(impactEffect, transform.position, transform.rotation);
//}

//public void TakeDamage(int damage)
//{
//    health -= damage;

//    if (health <= 0)
//    {
//        Die();
//    }
//}

//void Die()
//{
//    //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
//    Destroy(gameObject);
//}
//}

//if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) {
//    transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
//}
//if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
//{
//    transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
//}

//if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
//{
//    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
//}
//if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
//{
//    rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
//}

//if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
//{
//    rb.velocity = new Vector2(0f, rb.velocity.y);
//}
//if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
//{
//    rb.velocity = new Vector2(rb.velocity.x, 0f);
//}