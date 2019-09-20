using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour {
    public float speed;
    public int damage;
    public Rigidbody2D rb;
    private int timeToSurvive = 15;
    //public GameObject impactEffect;
    float scale = 1f;

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
        Player player = hitInfo.GetComponent<Player>();

        if (enemy != null)
        {
            Debug.Log("Colpito nemico con bomba");
            Destroy(hitInfo.gameObject);
        }
        else if (player != null)
        {
            Debug.Log("Colpito giocatore");
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);

    }
}
