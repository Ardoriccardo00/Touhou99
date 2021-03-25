using UnityEngine;
using Mirror;
using TMPro;

public class PlayerHealth : NetworkBehaviour
{
    [Header("Setup")]
    public PlayerIdentity playerSource;
    [SerializeField] TextMeshProUGUI healthText;

    [Header("Variables")]
    [SerializeField] float maxHealth;
    [SerializeField] [SyncVar] float currentHealth = 0;
    [SerializeField] float aodDamage = 20f;
    [SerializeField] int enemyCollisionDamage = 20;
    [SyncVar] bool isDead = false;

    //These delegate and event are created so the UI script can update the UI
    public delegate void HealthChangedDelegate(float currentHealth, float maxHealth);

    public event HealthChangedDelegate EventHealthChanged;

	private void Start()
	{
        SetHealth(maxHealth);
    }

    [ClientCallback] //Only clients can run it
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
        EventHealthChanged?.Invoke(currentHealth, maxHealth); //This is for the UI
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
            /*if (collision.gameObject.GetComponent<BulletBehaviour>().playerWhoShotMe != null) //This can't happen because players can't shoot themselves
            {
                var bulletHitPlayer = collision.gameObject.GetComponent<BulletBehaviour>().playerWhoShotMe.GetComponent<PlayerWeapon>();
                bulletHitPlayer.CmdIncreaseBomb(bulletHitPlayer.bombPowerToIncrease);
            }*/

            if (collision.gameObject.GetComponent<BulletBehaviour>().enemyWhoShotMe != null)
            {
                var newdamage = collision.GetComponent<BulletBehaviour>().bulletDamage;
                CmdTakeDamage(newdamage);
                var playerResponsibleForMyDeath = collision.GetComponent<BulletBehaviour>().playerWhoShotMe;
				if (isDead)
				{
                    playerResponsibleForMyDeath.GetComponent<PlayerWeapon>().playerKillCount++;
				}
            }            
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Enemy")
        {
            print("Collided with enemy");
            CmdTakeDamage(enemyCollisionDamage);
        }
    }

	private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AOD")
        {
            CmdTakeDamage(aodDamage * Time.deltaTime);
        }
    }
}
