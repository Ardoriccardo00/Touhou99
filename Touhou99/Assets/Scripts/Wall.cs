using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public int damage = 100;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        playerMovement player = hitInfo.GetComponent<playerMovement>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        else if (player != null)
        {
            player.TakeDamage(0);
        }
    }
}
