using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombWeapon : MonoBehaviour
{
    [Header("Components")]
    public Transform bombFirePoint;
    public GameObject bombPrefab;
    [System.Obsolete]  public PlayerWeapon weapon;

    [System.Obsolete]
    void start()
    {
        weapon = FindObjectOfType<PlayerWeapon>();
    }

    [System.Obsolete]
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (weapon.bombPower >= 40f)
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
