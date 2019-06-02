using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[System.Obsolete]
public class Weapon : NetworkBehaviour
{

    public Transform firePoint;
    public Transform firePoint2;
    public Transform bombFirePoint;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    [System.Obsolete]
    public playerMovement player;

    [System.Obsolete]
    void start()
    {
        player = FindObjectOfType<playerMovement>();
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (this.isLocalPlayer)
            {
                Shoot();
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

    void Bomb()
    {
        Instantiate(bombPrefab, bombFirePoint.position, bombFirePoint.rotation);
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
    }
}
