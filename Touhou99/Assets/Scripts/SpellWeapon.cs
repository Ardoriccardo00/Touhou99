using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject spellPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Spell();
        }
    }

    void Spell()
    {
        Instantiate(spellPrefab, firePoint.position, firePoint.rotation);
    }

}
