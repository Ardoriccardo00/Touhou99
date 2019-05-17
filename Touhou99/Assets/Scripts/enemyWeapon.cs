using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    float fireRate;
    float nextFire;

    void Start()
    {
        fireRate = 5f;
        nextFire = Time.time;
    }

    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.C))
         {
             Shoot();
         }*/
        CheckIfTimeToFire();

    }

    void CheckIfTimeToFire()
    {
        if (Time.time > nextFire)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        nextFire = Time.time + fireRate;
    }
}
