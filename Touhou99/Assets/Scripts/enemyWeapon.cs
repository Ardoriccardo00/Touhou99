using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class EnemyWeapon : NetworkBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    Player player;

    [Header("Statistics")]
    float fireRate = 0;
    private float nextFire;
    private float timeBetweenShoot;
    private float timeBetweenShootCounter;

    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        timeBetweenShoot = Random.Range(1f, 2.5f);
        timeBetweenShootCounter = timeBetweenShoot;
        nextFire = Time.time;
    }

    void Update()
    {
        timeBetweenShootCounter -= Time.deltaTime;
        if (player != null) { CheckIfTimeToFire(); }
        
    }

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

    //[Command]
    void CmdShoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
        nextFire = Time.time + fireRate;
    }
}
