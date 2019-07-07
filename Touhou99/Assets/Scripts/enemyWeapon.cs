﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    float fireRate = 0;
    float nextFire;
    private float timeBetweenShoot;
    private float timeBetweenShootCounter;
    [System.Obsolete]
    playerMovement player;

    [System.Obsolete]
    void Start()
    {
        player = GameObject.FindObjectOfType<playerMovement>();
        timeBetweenShoot = Random.Range(1f, 2.5f);
        timeBetweenShootCounter = timeBetweenShoot;
        nextFire = Time.time;
    }

    [System.Obsolete]
    void Update()
    {
        //timeBetweenShoot = Random.Range(1f, 5f);
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
                Shoot();
                timeBetweenShoot = Random.Range(1f, 5f);
                //Debug.Log(timeBetweenShoot);
                timeBetweenShootCounter = timeBetweenShoot;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        nextFire = Time.time + fireRate;
    }
}
