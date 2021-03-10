using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum EnemyType
{
    fairy,
    shield,
	exploder,
	turret
}

public class Enemy : NetworkBehaviour
{
	[Header("general enemy")]
	public EnemyType enemyType;
    [SerializeField] float moveSpeed = 5f;
	[SerializeField] Vector2 movement;
    Rigidbody2D rb;

	[Header("Shooting")]
	[SerializeField] BulletBehaviour bullet;
	[SerializeField] Transform shootingPoint;
	[SerializeField] float bulletForce = 20f;

	[Header("Exploder")]
	[SerializeField] float maxTimerToExplode = 3f;
	[SerializeField] float timerToExplode;
	[SerializeField] GameObject aodObject;

	[Header("Turret")]
	public PlayerIdentity targetPlayer;
	[SerializeField] float timerMaxToShoot = 1f;
	float timerToShoot;

	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
		timerToExplode = maxTimerToExplode;
		timerToShoot = timerMaxToShoot;

		if(enemyType == EnemyType.turret)
		{
			float distanceToClosestPlayer = Mathf.Infinity;
			PlayerIdentity[] allPlayers = FindObjectsOfType<PlayerIdentity>();

			foreach (PlayerIdentity currentPlayer in allPlayers)
			{
				float distanceToCenter = (currentPlayer.transform.position - transform.position).sqrMagnitude;
				if (distanceToCenter < distanceToClosestPlayer)
				{
					distanceToClosestPlayer = distanceToCenter;
					targetPlayer = currentPlayer.GetComponent<PlayerIdentity>();					
				}
			}
		}
	}

	private void Update()
	{
		switch (enemyType)
		{
			case EnemyType.fairy:
				//
				break;

			case EnemyType.shield:
				//movement = Vector2.down;
				break;

			case EnemyType.exploder:
				if (timerToExplode > 0)
				{
					timerToExplode -= Time.deltaTime;

					float timerPerc = timerToExplode / maxTimerToExplode * 100;
					GetComponent<SpriteRenderer>().color = new Color(timerPerc / 100, 0, 0);
				} 
				else Explode();
				break;

			case EnemyType.turret:
				LookAtPlayer();
				if (timerToShoot > 0) timerToShoot -= Time.deltaTime;
				else
				{
					ShootBullet();
					timerToShoot = timerMaxToShoot;
				}				
				break;
		}
	}

	

	private void FixedUpdate()
	{
		ExecuteMovement();
	}

	void ExecuteMovement()
	{
		rb.velocity = movement * moveSpeed * Time.deltaTime;
	}

	#region Collisons
	private void OnCollisionEnter2D(Collision2D collision)
	{
		switch (collision.gameObject.tag)
	    {
			case "Arena":
				print("Collided with arena");
				break;

			case "Player":
				print("Collided with player");
				break;
		}

		NetworkServer.Destroy(gameObject);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Bullet")
		{
			moveSpeed -= 10f;
		}
	}
	#endregion

	#region Exploders

	void Explode()
	{
		GameObject newAod = Instantiate(aodObject, transform.position, transform.rotation);

		NetworkServer.Destroy(gameObject);
		Destroy(gameObject);
		//Spawn Area of damage
	}

	#endregion

	#region Turret
	private void LookAtPlayer()
	{
		Vector2 direction = targetPlayer.transform.position - transform.position;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, moveSpeed * Time.deltaTime);
	}

	void ShootBullet()
	{
		BulletBehaviour newBullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
		newBullet.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * bulletForce, ForceMode2D.Impulse);
	}
	#endregion
}
