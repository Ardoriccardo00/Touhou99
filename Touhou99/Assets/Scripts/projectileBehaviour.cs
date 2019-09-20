using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[System.Obsolete]
public class projectileBehaviour : NetworkBehaviour
{
    public float speed;
    public int damage;
    public string shooter;
    //Player theClosestPlayer;
    //Weapon playersWeapon;
    GameObject playerToReward;
    //public GameObject impactEffect;

    void Start()
    {
        playerToReward = GameObject.Find(shooter);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.tag == "Enemy" || hitInfo.tag == "Clone")
        {
            playerToReward.GetComponent<Weapon>().bombPower += UnityEngine.Random.Range(0.5f, 0.8f); //was Player instead of Weapon

            if (hitInfo.tag == "Enemy")
            {
                NetworkServer.Destroy(hitInfo.gameObject);
            }

            else if(hitInfo.tag == "Clone")
            {
                CloneMovement cloneHit = FindObjectOfType<CloneMovement>();
                cloneHit.currentHealth -= damage;
                Debug.Log("vita clone: " + cloneHit.currentHealth);
            }         
            NetworkServer.Destroy(gameObject);
        }

        else if (hitInfo.tag == "Muro" || hitInfo.tag == "Clone")
        {
            NetworkServer.Destroy(gameObject);
        }

        //else if(hitInfo.tag == "Clone")
        //{
        //    //CloneMovement cloneHit = FindObjectOfType<CloneMovement>();
        //    //cloneHit.currentHealth -= damage;
        //    //Debug.Log("Salute clone: " + cloneHit.currentHealth);
        //    Debug.Log("clone colpito");
        //    NetworkServer.Destroy(hitInfo.gameObject);
        //    Destroy(gameObject);
        //}
        //Instantiate(impactEffect, transform.position, transform.rotation); 
        Destroy(gameObject);
    }
}