using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
	[Header("Setup")]
	public Transform[] arenaCorners;
    [SerializeField] Enemy[] enemiesToSpawn;

	[Header("Players")]
	//[SyncVar] public PlayerWeapon ownPlayer;
	public PlayerWeapon targetPlayer;
	public EnemySpawner targetPlayerSpawner;
	public Transform[] targetPlayerCorners;

	[Header("Timers")]
	[SerializeField] float timerToSpawnMax = 1f;
	float timerToSpawn = 0f;

	[SerializeField] float minTimeToSpawn = 1f;
	[SerializeField] float maxTimeToSpawn = 5f;

	[SerializeField] bool canSpawn = true;

	private void Awake()
	{
		timerToSpawn = timerToSpawnMax;
		//ownPlayer.cameraSwitcher.cameraSwitched += CameraSwitcher_cameraSwitched;
	}

	public void CameraSwitcher_cameraSwitched(PlayerWeapon ownerOfCameraSwitched)
	{
		print("Event called on enemy spawner");
		targetPlayer = ownerOfCameraSwitched;
		targetPlayerSpawner = targetPlayer.ownEnemySpawnPoint;
		targetPlayerCorners = targetPlayerSpawner.arenaCorners;
		CmdSpawnEnemy(targetPlayerSpawner.GetRandomEnemy(), true);

		//targetPlayer.ownEnemySpawnPoint.CmdSpawnEnemy(GetRandomEnemy()); //The enemy spawned should be the last killed and stored on a variable on player weaapon
	}

	private void Update()
	{
		UpdateTimerOnServer();
		if (Input.GetKeyDown(KeyCode.M))
		{
			CmdSpawnEnemy(GetRandomEnemy(), false);
		}
	}

	int ConvertEnemyTypeToInt(EnemyType type) //fairy, shield, exploder, turret
	{
		int newValue = 0;
		EnemyType[] enemyArray = (EnemyType[])System.Enum.GetValues(typeof(EnemyType));
		for(int i = 0; i < enemyArray.Length - 1; i++)
		{
			if(enemyArray[i] == type)
			{
				newValue = i;
			}
		}
		return newValue;
	}

	public EnemyType GetRandomEnemy()
	{
		EnemyType[] enemyArray = (EnemyType[])System.Enum.GetValues(typeof(EnemyType));
		int randomValue = Random.Range(0, enemyArray.Length);
		return enemyArray[randomValue];
	}

	[Command(ignoreAuthority = true)]
	public void CmdSpawnEnemy(EnemyType type, bool useTarget) //was int enemyIndex
	{
		//Generates a rendom pos inside the arena
		float posX, posY;
		if (!useTarget)
		{
			posX = Random.Range(arenaCorners[0].transform.position.x, arenaCorners[1].transform.position.x);
			posY = Random.Range(arenaCorners[0].transform.position.y, arenaCorners[2].transform.position.y);
		}
		else
		{
			posX = Random.Range(targetPlayerSpawner.arenaCorners[0].transform.position.x, targetPlayerSpawner.arenaCorners[1].transform.position.x);
			posY = Random.Range(targetPlayerSpawner.arenaCorners[0].transform.position.y, targetPlayerSpawner.arenaCorners[2].transform.position.y);
			/*posX = Random.Range(targetPlayerSpawner.arenaCorners[0].transform.localPosition.x, targetPlayerSpawner.arenaCorners[1].transform.localPosition.x);
			posY = Random.Range(targetPlayerSpawner.arenaCorners[0].transform.localPosition.y, targetPlayerSpawner.arenaCorners[2].transform.localPosition.y);*/
		}
		/*float posX = Random.Range(newArenaCorners[0].transform.position.x, newArenaCorners[1].transform.position.x);
		float posY = Random.Range(newArenaCorners[0].transform.position.y, newArenaCorners[2].transform.position.y);*/

		Vector2 newSpawnPoint = new Vector2(posX, posY);

		int enemyIndex = ConvertEnemyTypeToInt(type);

		print("Spawning enemy at coordinates x:" + newSpawnPoint.x + " Y:" + newSpawnPoint.y);
		var newEnemy = Instantiate(enemiesToSpawn[enemyIndex], newSpawnPoint, Quaternion.identity);
		NetworkServer.Spawn(newEnemy.gameObject);

		newEnemy.enemyHasDied += NewEnemy_enemyHasDied;
	}

	private void NewEnemy_enemyHasDied(EnemyType typeOfDeadEnemy)
	{
		print("Event happened on enemy spawner script, spawning" + typeOfDeadEnemy);
		CmdSpawnEnemy(targetPlayerSpawner.GetRandomEnemy(), true);
	}

	void UpdateTimerOnServer()
	{
		if(timerToSpawn > 0f)
		{
			timerToSpawn -= Time.deltaTime;
		}
		else
		{
			if (!canSpawn) return;
			CmdSpawnEnemy(GetRandomEnemy(), false);

			//Changes the timer value and resets it
			timerToSpawnMax = Random.Range(minTimeToSpawn, maxTimeToSpawn);
			timerToSpawn = timerToSpawnMax;
		}
		
	}
}
