using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class projectileBehaviour : NetworkBehaviour
{
    public float speed;
    public int damage;
    //public GameObject impactEffect;

    void Start()
    {
        //transform.rotation = Quaternion.LookRotation(transform.up) * transform.rotation;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();
        NetworkIdentity ni = GetComponent<NetworkIdentity>();
        projectileBehaviour pb = GetComponent<projectileBehaviour>();

        if (hitInfo.tag == "Enemy")
         {
            player = FindObjectOfType<playerMovement>();
            player.bombPower = player.bombPower + UnityEngine.Random.Range(1f, 5f);
            Debug.Log(player.bombPower);
            Destroy(enemy);
        }
         else if (hitInfo.tag == "Muro")
        {
            Destroy(gameObject);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject); 
    }
}