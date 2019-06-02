using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    public Transform bombFirePoint;
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
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (player.bombPower >= 40f)
            {
                Bomb();
            }
            else { Debug.Log("no"); }
        }
    }

    void Bomb()
    {
        //Instantiate(bombPrefab, firePoint.position, firePoint.rotation);
    }

}
