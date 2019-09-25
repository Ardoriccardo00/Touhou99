using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class EnemyWeapon : NetworkBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    float fireRate = 0;
    float nextFire;
    private float timeBetweenShoot;
    private float timeBetweenShootCounter;
    [System.Obsolete]
    Player player;

    [System.Obsolete]
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        timeBetweenShoot = Random.Range(1f, 2.5f);
        timeBetweenShootCounter = timeBetweenShoot;
        nextFire = Time.time;
    }

    [System.Obsolete]
    void Update()
    {
        timeBetweenShootCounter -= Time.deltaTime;
        if (player != null) { CheckIfTimeToFire(); }
        
    }

    [System.Obsolete]
    void CheckIfTimeToFire()
    {
        if (player != null)
        {
            if (timeBetweenShootCounter <= 0)
            {
                CmdShoot();
                timeBetweenShoot = Random.Range(1f, 5f);
                timeBetweenShootCounter = timeBetweenShoot;
            }
        }
    }

    [Command]
    [System.Obsolete]
    void CmdShoot()
    {
        print("enemy shoots!");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
        nextFire = Time.time + fireRate;
    }
}
