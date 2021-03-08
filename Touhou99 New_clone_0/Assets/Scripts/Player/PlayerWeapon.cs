using UnityEngine;
using Mirror;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;

    [SerializeField] CloneBehaviour clone;

    [SerializeField] TextMeshProUGUI bombText;

    [SerializeField] float shootingDelay = 1f;
    float shootingDelayTimer = 0;

    [SerializeField] float maxBombPower = 100f;
    [SyncVar] [SerializeField] [Range(0f, 100f)] public float bombPower = 0f;
    public float bombPowerToIncrease = 5f;

    public delegate void BombPowerChangeDelegate(float bombPower, float maxBombPower);
    public event BombPowerChangeDelegate EventBombPowerChanged;

    public PlayerIdentity targetPlayer;

    [Header("Arena Components")]
    public GameObject playerArena;
    public GameObject cloneSpawnPoint;
    public GameObject enemySpawnPoint;

	public override void OnStartServer()
	{
        shootingDelayTimer = shootingDelay;
        bombText.text = "Bomb: " + Mathf.RoundToInt(bombPower) + "%";
    }

    public void FindOwnPlayerArena()
    {
        float distanceToClosestPoint = Mathf.Infinity;
        GameObject[] allCenters = GameObject.FindGameObjectsWithTag("Arena");

        foreach (GameObject currentCenter in allCenters)
        {
            float distanceToCenter = (currentCenter.transform.position - transform.position).sqrMagnitude;
            if (distanceToCenter < distanceToClosestPoint)
            {
                distanceToClosestPoint = distanceToCenter;
                playerArena = currentCenter;
                cloneSpawnPoint = playerArena.transform.Find("Clone Spawn Point").gameObject;
                cloneSpawnPoint = playerArena.transform.Find("Enemy Spawn Point").gameObject;
            }
        }
    }

    [ClientCallback]
    void Update()
    {
		if (isLocalPlayer)
		{
			if (Input.GetKey(KeyCode.Z))
			{
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

            if (Input.GetKeyDown(KeyCode.M))
            {
                CmdIncreaseBomb(100);
            }
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

    [Command]
    public void CmdResetBomb()
	{
        SetBomb(0);
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
        var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newbullet.GetComponent<BulletBehaviour>().playerWhoShotMe = GetComponent<PlayerIdentity>();
        NetworkServer.Spawn(newbullet);
	}

    [Command]
	void CmdBomb()
    {
        if(bombPower == maxBombPower)
		{
            GameObject newClone;
            //GameObject newClone = Instantiate(clone.gameObject, transform.position, transform.rotation);           
            if (targetPlayer == null)
			{
                newClone = Instantiate(clone.gameObject, playerArena.transform.position, playerArena.transform.rotation);
                NetworkServer.Spawn(newClone);
            }
			else
			{
                GameObject enemyArenaCenter = targetPlayer.GetComponent<PlayerWeapon>().playerArena;
                newClone = Instantiate(clone.gameObject, enemyArenaCenter.transform.position, enemyArenaCenter.transform.rotation);
                NetworkServer.Spawn(newClone);
            }
            CmdResetBomb();
        }
    }

    public void PlayerKilledSomeone(EnemyType type)
	{
		print(type);
		if (targetPlayer == null) return;
		targetPlayer.GetComponent<PlayerWeapon>().enemySpawnPoint.GetComponent<EnemySpawner>().CmdSpawnEnemy(type);
		//Instantiate
	}
}
