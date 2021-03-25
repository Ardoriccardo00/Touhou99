using UnityEngine;
using Mirror;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;

public class PlayerWeapon : NetworkBehaviour
{
    [Header("Own Components")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI bombText;

    [Header("Shooting and spawing")]
    [SerializeField] CloneBehaviour clone;
    [SerializeField] float shootingDelay = 1f;
    float shootingDelayTimer = 0;

    [SerializeField] float maxBombPower = 100f;
    [SerializeField] [Range(0f, 100f)] public float bombPower = 0f;
    public float bombPowerToIncrease = 5f;

    public delegate void BombPowerChangeDelegate(float bombPower, float maxBombPower);
    public event BombPowerChangeDelegate EventBombPowerChanged;

    [Header("Arena Components")]
    public GameObject playerArena;
    public GameObject ownCloneSpawnPoint;
    public EnemySpawner ownEnemySpawnPoint;

    [SyncVar] public PlayerWeapon targetPlayer;
    public EnemySpawner targetPlayerEnemySpawnPoint;
    public GameObject targetPlayerCloneSpawnPoint;

    [Header("Other")]
    [SyncVar] public int eneyKillCount = 0;
    [SyncVar] public int playerKillCount = 0;

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

    public void FindOwnPlayerArena() //This functions find the own player arena, clone SP (spawn point) and own SP
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
                ownCloneSpawnPoint = playerArena.transform.Find("Clone Spawn Point").gameObject;
                ownEnemySpawnPoint = playerArena.GetComponentInChildren<EnemySpawner>();
                ownEnemySpawnPoint.ownPlayer = this;
            }
        }
    }

    //[Command]
    public void SetTargetPlayer(PlayerWeapon player) //Sets the target player and it's enemy spawn point
	{
        print("Set target player");
        targetPlayer = player;
        targetPlayerEnemySpawnPoint = targetPlayer.ownEnemySpawnPoint;
        targetPlayerCloneSpawnPoint = targetPlayer.ownCloneSpawnPoint;
    }

	#region Shooting
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

	[Command]
	void CmdShoot()
	{
        var newBullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newBullet.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * 20f, ForceMode2D.Impulse);
        newBullet.GetComponent<BulletBehaviour>().playerWhoShotMe = GetComponent<PlayerIdentity>();
        NetworkServer.Spawn(newBullet);
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
	#endregion

	public void PlayerKilledSomething()
	{
        eneyKillCount++;
	}

    //Create player killed player
}
