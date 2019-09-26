using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[System.Obsolete]
public class ProjectileBehaviour : NetworkBehaviour
{
    public float speed;
    public int damage;
    public string shooter;
    GameObject playerToReward;

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
                DamageClone();
            }
        }       

        if(hitInfo.tag == "EnemyBullet")
        {
            Destroy(hitInfo.gameObject);
            NetworkServer.Destroy(hitInfo.gameObject);
        }
           NetworkServer.Destroy(gameObject);
           Destroy(gameObject);
    }

    private void DamageClone()
    {
        CloneMovement cloneHit = FindObjectOfType<CloneMovement>();
        cloneHit.currentHealth -= damage;
        Debug.Log("vita clone: " + cloneHit.currentHealth);
    }
}