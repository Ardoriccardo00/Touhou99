using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour {
    [Header("Statistics")]
    public float speed;
    public int damage;
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

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Enemy" || hitInfo.tag == "EnemyBullet")
        {
            Debug.Log("Colpito nemico con bomba");
            Destroy(hitInfo.gameObject);
        }

        //Instantiate(impactEffect, transform.position, transform.rotation);
    }
}
