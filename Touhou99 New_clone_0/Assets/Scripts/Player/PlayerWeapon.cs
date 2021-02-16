using UnityEngine;
using Mirror;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;

    [SerializeField] TextMeshProUGUI bombText;

    [SerializeField] float shootingDelay = 1f;
    float shootingDelayTimer = 0;

    [SyncVar] [SerializeField] float maxBombPower = 100f;
    [SerializeField] [Range(0f, 100f)] float bombPower = 0f;

    public delegate void BombPowerChangeDelegate(float bombPower, float maxBombPower);
    public event BombPowerChangeDelegate EventBombPowerChanged;

	public override void OnStartServer()
	{
        shootingDelayTimer = shootingDelay;
        bombText.text = "Bomb: " + Mathf.RoundToInt(bombPower) + "%";
    }

    [ClientCallback]
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

			if (Input.GetKeyDown(KeyCode.X))
			{
                CmdBomb();
			}

/*            if (Input.GetKeyDown(KeyCode.Space))
            {
                FindObjectOfType<MatchInitializer>().CmdSpawnArenas();
            }*/
        }
    }

    [Server] 
    private void SetBomb(float value)
	{
        bombPower = value;
        EventBombPowerChanged?.Invoke(bombPower, maxBombPower);
	}

	[Command]
	public void CmdIncreaseBomb(float valueToIncrease)
	{
		SetBomb(bombPower += valueToIncrease);
	}

	/*[Server]
    private void ShootBullet()
	{
        SetBomb(bombPower += 1f);
        var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newbullet.GetComponent<BulletBehaviour>().playerWhoShotMe = GetComponent<PlayerIdentity>();
        Destroy(newbullet, 3f);
    }*/

	[Command]
	void CmdShoot()
	{
        //print("Client: Asking to shoot");
        SetBomb(bombPower += 1f);
        var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newbullet.GetComponent<BulletBehaviour>().playerWhoShotMe = GetComponent<PlayerIdentity>();
        NetworkServer.Spawn(newbullet);
        StartCoroutine(DestroyBullet(newbullet));
        /*Destroy(newbullet, 3f);
        NetworkServer.Destroy(newbullet);*/
		//RpcShoot();
	}

    IEnumerator DestroyBullet(GameObject objectToDestroy)
	{
        yield return new WaitForSeconds(3);
        Destroy(objectToDestroy);
        NetworkServer.Destroy(objectToDestroy);
    }

	/*[ClientRpc]
	void RpcShoot()
	{
        //print("Server: Shooting");
        //CmdIncreaseBomb(1f);
        SetBomb(bombPower += 1f);
		var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
		newbullet.GetComponent<BulletBehaviour>().playerWhoShotMe = GetComponent<PlayerIdentity>();
		Destroy(newbullet, 3f);
	}*/

	void CmdBomb()
    {
        RpcBomb();
    }

    [ClientRpc]
    void RpcBomb()
    {
        print("Boom!");
    }

    /*[Command]
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
	}*/
}
