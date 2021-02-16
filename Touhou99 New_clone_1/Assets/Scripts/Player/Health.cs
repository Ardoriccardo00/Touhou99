using UnityEngine;
using Mirror;
using TMPro;

public class Health : NetworkBehaviour
{
    [Header("Setup")]
    BoxCollider2D hitBox;
    public PlayerIdentity playerSource;
    [SerializeField] TextMeshProUGUI healthText;

    [Header("Variables")]
    [SerializeField] float maxHealth;
    [SerializeField] [SyncVar] float currentHealth = 0;

    //These delegate and event are created so the UI script can update the UI
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);

    public event HealthChangedDelegate EventHealthChanged;

	public override void OnStartServer()
	{
		hitBox = GetComponent<BoxCollider2D>();
		SetHealth(maxHealth);
	}

	[ClientCallback]
    private void Update()
    {
        if (!hasAuthority) return;
    }

    //whenever the server updates the health it will raise an event
    //and all the clients subscribed to it will receive it
    [Server]
    private void SetHealth(float value)
	{
        currentHealth = value;
        EventHealthChanged?.Invoke(currentHealth, maxHealth);
	}

    [Command]
    public void CmdTakeDamage(float damageToDeal)
	{
        SetHealth(currentHealth -= damageToDeal);
        if (currentHealth <= 0) print(transform.name + " has died");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!isLocalPlayer) return;

		else if(collision.tag == "Bullet")
		{
            var newdamage = collision.GetComponent<BulletBehaviour>().bulletDamage;
            CmdTakeDamage(newdamage);
		}
	}

	/*[Command]
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
	}*/
}
