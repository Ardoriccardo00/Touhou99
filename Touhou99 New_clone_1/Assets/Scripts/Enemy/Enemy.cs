using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum EnemyType
{
    fairy,
    shield,
	exploder
}

public class Enemy : NetworkBehaviour
{
	[Header("general enemy")]
    [SerializeField] EnemyType enemyType;
    [SerializeField] float moveSpeed = 5f;
	[SerializeField] Vector2 movement;
    Rigidbody2D rb;

	[Header("Exploder")]
	[SerializeField] float maxTimerToExplode = 3f;
	[SerializeField] float timerToExplode;
	[SerializeField] GameObject aodObject;

	private void Start()
	{
        rb = GetComponent<Rigidbody2D>();
		timerToExplode = maxTimerToExplode;
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
					print(timerPerc);
					GetComponent<SpriteRenderer>().color = new Color(timerPerc / 100, 0, 0);
				} 
				else Explode();
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
}
