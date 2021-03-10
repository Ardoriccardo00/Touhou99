using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
	[SerializeField] Transform[] arenaCorners;

    [SerializeField] Enemy[] enemiesToSpawn;


	[Header("Timers")]
	[SerializeField] float timerToSpawnMax = 1f;
	float timerToSpawn = 0f;

	[SerializeField] float minTimeToSpawn = 1f;
	[SerializeField] float maxTimeToSpawn = 5f;

	[SerializeField] bool canSpawn = true;

	private void Start()
	{
		timerToSpawn = timerToSpawnMax;
	}

	private void Update()
	{
		UpdateTimerOnServer();
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

	EnemyType GetRandomEnemy()
	{
		EnemyType[] enemyArray = (EnemyType[])System.Enum.GetValues(typeof(EnemyType));
		int randomValue = Random.Range(0, enemyArray.Length);
		return enemyArray[randomValue];
	}

	[Command(ignoreAuthority = true)]
	public void CmdSpawnEnemy(EnemyType type) //was int enemyIndex
	{
		//Generates a rendom pos inside the arena
		float posX = Random.Range(arenaCorners[0].transform.position.x, arenaCorners[1].transform.position.x);
		float posY = Random.Range(arenaCorners[0].transform.position.y, arenaCorners[2].transform.position.y);

		Vector2 newSpawnPoint = new Vector2(posX, posY);

		int enemyIndex = ConvertEnemyTypeToInt(type);

		var newEnemy = Instantiate(enemiesToSpawn[enemyIndex], newSpawnPoint, Quaternion.identity);
		NetworkServer.Spawn(newEnemy.gameObject);
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
			CmdSpawnEnemy(GetRandomEnemy());

			//Changes the timer value and resets it
			timerToSpawnMax = Random.Range(minTimeToSpawn, maxTimeToSpawn);
			timerToSpawn = timerToSpawnMax;
		}
		
	}
}
