using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehaviour : MonoBehaviour
{
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    //public GameObject impactEffect;

    void Start()
    {
        rb.velocity = transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (enemy != null)
         {
             enemy.TakeDamage(damage);
             Debug.Log("Colpito nemico");
         }
         else if (player != null)
        {
            Debug.Log("Colpito giocatore");
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject); 
    }
}
