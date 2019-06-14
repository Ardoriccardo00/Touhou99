using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class projectileBehaviour : NetworkBehaviour
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
        NetworkIdentity ni = GetComponent<NetworkIdentity>();
        projectileBehaviour pb = GetComponent<projectileBehaviour>();

        if (enemy != null)
         {
            player = FindObjectOfType<playerMovement>();
            enemy.TakeDamage(damage);
            player.bombPower = player.bombPower + UnityEngine.Random.Range(1f, 5f);
            Debug.Log(player.bombPower);
        }
         else if (player != null)
        {
            Debug.Log("Colpito giocatore");
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject); 
    }
}