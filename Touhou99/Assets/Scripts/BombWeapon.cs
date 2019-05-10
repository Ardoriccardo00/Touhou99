using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bombPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Bomb();
        }
    }

    void Bomb()
    {
        Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
    }

}
