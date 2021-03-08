﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
    [Header("Variables")]
    [SerializeField] float maxHealth;
    [SerializeField] [SyncVar] float currentHealth = 0;
    [SerializeField] float aodDamage = 5f;

    public override void OnStartServer()
    {
        SetHealth(maxHealth);
    }

    [Server]
    private void SetHealth(float value)
    {
        currentHealth = value;
    }

    //[Command]
    public void CmdTakeDamage(float damageToDeal)
    {
        //SetHealth(currentHealth -= damageToDeal);
        currentHealth -= damageToDeal;
        if (currentHealth <= 0)
		{
            Die();
		} 
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        print("Trigger");
        if (collision.tag == "Bullet")
        {
            if(collision.gameObject.GetComponent<BulletBehaviour>().playerWhoShotMe != null) //If the bullet was shot by a player
			{
                PlayerWeapon playerWhoShotBullet = collision.gameObject.GetComponent<BulletBehaviour>().playerWhoShotMe.GetComponent<PlayerWeapon>();
                playerWhoShotBullet.CmdIncreaseBomb(playerWhoShotBullet.bombPowerToIncrease);
                playerWhoShotBullet.PlayerKilledSomeone(GetComponent<Enemy>().enemyType);
            }
            
            var newdamage = collision.GetComponent<BulletBehaviour>().bulletDamage;
            print("damage: " + newdamage);
            CmdTakeDamage(newdamage);
        }
    }

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "AOD")
		{
            CmdTakeDamage(aodDamage * Time.deltaTime);
		}
	}

	public void Die()
	{
        Destroy(gameObject);
        NetworkServer.Destroy(gameObject);
	}
}
