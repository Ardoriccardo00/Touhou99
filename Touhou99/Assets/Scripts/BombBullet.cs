using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour {
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    private int timeToSurvive = 75;
    //public GameObject impactEffect;
    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    void Update()
    {
        timeToSurvive--;
        if (timeToSurvive <= 0) { Destroy(gameObject); }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Colpito nemico");
            //Destroy(gameObject);
        }
        else if (player != null)
        {
            Debug.Log("Colpito giocatore");
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
