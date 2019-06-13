using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[System.Obsolete]
public class Weapon : NetworkBehaviour
{

    public Transform firePoint;
    public Transform firePoint1;
    public Transform bombFirePoint;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    [System.Obsolete]
    public playerMovement player;

    public float distance = 100f;

    public float fireRate = 0f;

    [System.Obsolete]
    void start()
    {
        player = FindObjectOfType<playerMovement>();
    }

    [System.Obsolete]
    void Update()
    {
        if(fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (this.isLocalPlayer)
                {
                    Shoot();
                }

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating ("Shoot", 0f, 1f/fireRate);
            }
            else if (Input.GetButtonDown("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.bombPower >= 40f)
            {
                Bomb();
                player.bombPower = player.bombPower - 40f;
            }
            else { Debug.Log("no"); }
        }

    }

    [Client]
    void Bomb()
    {
        Instantiate(bombPrefab, bombFirePoint.position, bombFirePoint.rotation);
    }

    [Client]
    void Shoot()
    {
        Debug.Log("shoot");
        //GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject bullet2 =  Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Debug.DrawLine(transform.position, transform.position + transform.up);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position + transform.up * distance, Mathf.Infinity);
        if (hit.collider.tag == "Player")
        {
            CmdPlayerShot(hit.collider.name);
        }

        if (hit.collider.tag == "Enemy")
        {
            CmdEnemyShot(hit.collider.name);
        }
    }

    [Command]
    void CmdEnemyShot(string _ID)
    {
        Debug.Log(_ID + " has been shot");
    }

    [Command]
    void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + " enemy has been shot");
    }
}
