using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    public Transform bombFirePoint;
    public GameObject bombPrefab;
    public playerMovement player;

    void start()
    {
        player = FindObjectOfType<playerMovement>();
    }
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
