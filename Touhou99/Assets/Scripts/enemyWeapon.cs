using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
