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
    playerMovement player;
    private int timeToSurvive = 150;


    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        moveDirection = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
    }

    void Update()
    {
        timeToSurvive--;
        if (timeToSurvive <= 0) { Destroy(gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Debug.Log("Colpito giocatore");
            Destroy(gameObject);
        }
        else if (enemy != null)
        {
            Debug.Log("Colpito nemico");

        }
       
        //Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
