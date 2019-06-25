using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour {
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    private int timeToSurvive = 75;
    //public GameObject impactEffect;
    float scale = 1f;
    void Start()
    {
        //rb.velocity = transform.up * speed;
    }

    void Update()
    {
        transform.localScale += new Vector3(scale, scale, scale);
        timeToSurvive--;
        if (timeToSurvive <= 0) { Destroy(gameObject); }
        scale += 1f;
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (enemy != null)
        {
            Debug.Log("Colpito nemico con bomba");
            Destroy(enemy);
        }
        else if (player != null)
        {
            Debug.Log("Colpito giocatore");
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
