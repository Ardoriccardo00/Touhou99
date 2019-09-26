using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    public int damage = 100;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        Player player = hitInfo.GetComponent<Player>();

        if (enemy != null)
        {
            Destroy(enemy);
        }
    }
}
