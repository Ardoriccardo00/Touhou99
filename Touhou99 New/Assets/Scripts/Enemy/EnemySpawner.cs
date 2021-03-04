using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
	[SerializeField] Transform[] arenaCorners;

    [SerializeField] Enemy[] enemiesToSpawn;

	[SerializeField] float timerToSpawnMax = 1f;
	float timerToSpawn = 0f;

	[SerializeField] float minTimeToSpawn = 1f;
	[SerializeField] float maxTimeToSpawn = 5f;

	private void Start()
	{
		timerToSpawn = timerToSpawnMax;
	}

	private void Update()
	{
		UpdateTimerOnServer();
	}

	[Command(ignoreAuthority = true)]
	public void CmdSpawnEnemy(int enemyIndex, Vector2 spawnPos)
	{
		var newEnemy = Instantiate(enemiesToSpawn[enemyIndex], spawnPos, Quaternion.identity);
		NetworkServer.Spawn(newEnemy.gameObject);
	}

	//[Server]
	void UpdateTimerOnServer()
	{
		if(timerToSpawn > 0f)
		{
			timerToSpawn -= Time.deltaTime;
		}
		else
		{
			//Generates a rendom pos inside the arena
			float posX = Random.Range(arenaCorners[0].transform.position.x, arenaCorners[1].transform.position.x);
			float posY = Random.Range(arenaCorners[0].transform.position.y, arenaCorners[2].transform.position.y);

			Vector2 newSpawnPoint = new Vector2(posX, posY);
			//print(newSpawnPoint);

			//Generates a random enemy prefab to spawn
			int enemyIndex = Random.Range(0, enemiesToSpawn.Length - 1);
			CmdSpawnEnemy(enemyIndex, newSpawnPoint);

			//Changes the timer value and resets it
			timerToSpawnMax = Random.Range(minTimeToSpawn, maxTimeToSpawn);
			timerToSpawn = timerToSpawnMax;
		}
		
	}
}
