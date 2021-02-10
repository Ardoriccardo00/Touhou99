using UnityEngine;
using Mirror;
using TMPro;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;

    [SerializeField] TextMeshProUGUI bombText;

    [SerializeField] float shootingDelay = 1f;
    float shootingDelayTimer = 0;

    [SerializeField] [Range(0f, 100f)] float bombPower = 0f;

    void Start()
    {
        shootingDelayTimer = shootingDelay;
        bombText.text = "Bomb: " + Mathf.RoundToInt(bombPower) + "%";
    }

    void Update()
    {
		if (isLocalPlayer)
		{
			if (Input.GetKey(KeyCode.Z))
			{
                //print(shootingDelayTimer);
                shootingDelayTimer -= Time.deltaTime;
                if (shootingDelayTimer > 0) return;
				else
				{
                    CmdShoot();
                    shootingDelayTimer = shootingDelay;
                }
			}
		}
    }

    [Command]
    void CmdShoot()
	{
		//print("Client: Asking to shoot");
		RpcShoot();
	}

    [ClientRpc]
    void RpcShoot()
	{
        //print("Server: Shooting");
        var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newbullet.GetComponent<BulletBehaviour>().playerWhoShotMe = GetComponent<PlayerIdentity>();
        Destroy(newbullet, 3f);
    }

    [Command]
    void CmdBomb()
	{
        RpcBomb();
	}

    [ClientRpc]
    void RpcBomb()
	{
        CmdResetBombPower();
        print("Boom!");
	}

    [Command]
    void CmdIncreaseBombPower(float increase)
	{
        RpcIncreaseBombPower(increase);
	}

    [ClientRpc]
    void RpcIncreaseBombPower(float increase)
	{
        bombPower += increase;
        bombText.text = "Bomb: " + Mathf.RoundToInt(bombPower) + "%";
    }

    [Command]
    void CmdResetBombPower()
	{
        RpcResetBombPower();
	}

    [ClientRpc]
    void RpcResetBombPower()
	{
        bombPower = 0;
        bombText.text = "Bomb: " + Mathf.RoundToInt(bombPower) + "%";
    }

    [Command]
	public void CmdDamagePlayer(PlayerIdentity target, float damageDealt)
	{
		DamagePlayer(target, damageDealt);
        CmdIncreaseBombPower(UnityEngine.Random.Range(1f, 5f));
		//bombPower += UnityEngine.Random.Range(1f, 5f);
        bombText.text = "Bomb: " + Mathf.RoundToInt(bombPower) + "%";
    }

    [ClientRpc]
	public void DamagePlayer(PlayerIdentity player, float damage)
	{
		var targetPlayerHealth = player.GetComponent<Health>();
		targetPlayerHealth.CmdDecreaseHealth(damage);
	}
}
