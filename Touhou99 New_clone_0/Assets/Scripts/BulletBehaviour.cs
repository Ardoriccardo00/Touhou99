using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletBehaviour : NetworkBehaviour
{
	[Header("Setup")]
    public Collider2D myCollider;
	public Rigidbody2D rb;
    public PlayerIdentity playerWhoShotMe;
	Transform bulletsPrefab;

	[Header("Stats")]
	public float bulletDamage = 10;

	[SerializeField] float timeToSurviveMax = 2f;
	float timeToSurviveTimer = 0;

	private void Start()
	{
		bulletsPrefab = GameObject.FindGameObjectWithTag("Bullet Parent").transform;
		transform.SetParent(bulletsPrefab);

		timeToSurviveTimer = timeToSurviveMax;

		myCollider = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (timeToSurviveTimer > 0)
		{
			timeToSurviveTimer -= Time.deltaTime;
		}
		else
		{
			Destroy(gameObject);
			NetworkServer.Destroy(gameObject);
		}
	}
}
