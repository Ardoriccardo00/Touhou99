using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
    BoxCollider2D hitBox;

    [SerializeField] float maxHealth;
    float currentHealth;

    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.tag == "Bullet")
		{
            float newDamage = collision.gameObject.GetComponent<BulletBehaviour>().bulletDamage;
            CmdDecreaseHealth(newDamage);
        }     
	}

	[Command]
    public void CmdIncreaseHealth(float value)
	{
        RpcIncreaseHealth(value);
	}

    [ClientRpc]
    void RpcIncreaseHealth(float value)
	{
        currentHealth += value;
	}

    [Command]
    public void CmdDecreaseHealth(float value)
    {
        RpcDecreaseHealth(value);
    }

    [ClientRpc]
    void RpcDecreaseHealth(float value)
    {
        currentHealth -= value;
    }

    [Command]
    void CmdDie()
	{
        RpcDie();
	}

    [ClientRpc]
    void RpcDie()
	{
        Destroy(gameObject);
	}
}
