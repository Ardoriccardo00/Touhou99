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
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (hitInfo.tag == "Enemy" || hitInfo.tag == "Clone")
        {
            player = FindObjectOfType<playerMovement>();
            player.bombPower = player.bombPower + UnityEngine.Random.Range(0.1f, 0.5f);

            Debug.Log(player.bombPower);

            if(hitInfo.tag == "Enemy")
            {
                Enemy enemy = FindObjectOfType<Enemy>();
                enemy.currentHealth -= damage;
            }
            else if(hitInfo.tag == "Clone")
            {
                CloneMovement cloneHit = FindObjectOfType<CloneMovement>();
                cloneHit.currentHealth -= damage;
                Debug.Log("vita clone: " + cloneHit.currentHealth);
            }
            //NetworkServer.Destroy(hitInfo.gameObject);
            //Destroy(gameObject);
        }

        else if (hitInfo.tag == "Muro" || hitInfo.tag == "Clone")
        {
            Destroy(gameObject);
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