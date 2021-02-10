using UnityEngine;
using Mirror;
using TMPro;

public class Health : NetworkBehaviour
{
    BoxCollider2D hitBox;

    [SerializeField] float maxHealth;
    float currentHealth;

    public PlayerIdentity playerSource;

    [SerializeField] TextMeshProUGUI healthText;

    void Start()
    {
        hitBox = GetComponent<BoxCollider2D>();
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
    }

	/*private void OnCollisionEnter2D(Collision2D collision)
	{
        if(collision.gameObject.tag == "Bullet")
		{
            float newDamage = collision.gameObject.GetComponent<BulletBehaviour>().bulletDamage;
            CmdDecreaseHealth(newDamage);
        }     
	}*/

	[Command]
    public void CmdIncreaseHealth(float value)
	{
        RpcIncreaseHealth(value);
	}

    [ClientRpc]
    void RpcIncreaseHealth(float value)
	{
        currentHealth += value;
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
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
        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        if (currentHealth <= 0)
		{
            CmdDie();
		}
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
		print(gameObject.name + "has died");
	}
}
