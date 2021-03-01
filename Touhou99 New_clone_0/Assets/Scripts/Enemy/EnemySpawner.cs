using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemySpawner : NetworkBehaviour
{
    [SerializeField] Enemy[] enemiesToSpawn;

	[SerializeField] float timerToSpawnMax = 1f;
	float timerToSpawn = 0f;

	private void Start()
	{
		timerToSpawn = timerToSpawnMax;
	}

	private void Update()
	{
		UpdateTimerOnServer();
		/*print(transform.lossyScale.x);
		print(transform.lossyScale.y);*/
	}

	[ClientRpc]
    public void RpcSpawnEnemy(int enemyIndex)
	{
		var newEnemy = Instantiate(enemiesToSpawn[enemyIndex], transform.position, transform.rotation);
		NetworkServer.Spawn(newEnemy.gameObject);
	}

	[Server]
	void UpdateTimerOnServer()
	{
		if(timerToSpawn > 0f)
		{
			timerToSpawn -= Time.deltaTime;
		}
		else
		{
			//int enemyIndex = Random.Range(0, enemiesToSpawn.Length - 1);
			RpcSpawnEnemy(0);
			ResetTimerOnServer();
		}
		
	}

	[Server]
	void ResetTimerOnServer()
	{
		timerToSpawn = timerToSpawnMax;
	}

	
}
