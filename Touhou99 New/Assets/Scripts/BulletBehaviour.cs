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
	[SerializeField] float moveSpeed = 1000;
	Vector2 direction;
	public float bulletDamage = 10;
	[SerializeField] MovingDirection movingDirection;

	[SerializeField] float timeToSurviveMax = 2f;
	float timeToSurviveTimer = 0;

	public enum MovingDirection
	{
		up,
		down,
		left,
		right
	}

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

		switch (movingDirection)
		{
			case MovingDirection.up:
				direction = Vector2.up;
				break;
			case MovingDirection.down:
				direction = Vector2.down;
				break;
			case MovingDirection.left:
				direction = Vector2.left;
				break;
			case MovingDirection.right:
				direction = Vector2.right;
				break;
		}

		rb.velocity = direction * moveSpeed * Time.deltaTime;
	}
}
