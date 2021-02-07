using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletBehaviour : NetworkBehaviour
{
    public Collider2D myCollider;
	public Rigidbody2D rb;
    PlayerIdentity playerWhoShotMe;
	[SerializeField] float moveSpeed = 1000;
	//PlayerIdentity playerIHit;

	private void Start()
	{
		myCollider = GetComponent<Collider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		rb.velocity = Vector2.up * moveSpeed * Time.deltaTime;
	}

	/*void OnCollisionEnter2D(Collision2D collision)
	{
		Destroy(gameObject);
	}*/

	public void SetPlayerWhoShotMe(PlayerIdentity player)
	{
        playerWhoShotMe = player;
	}

    public PlayerIdentity ReturnPlayerWhoShotMe()
	{
        return playerWhoShotMe;
	}
}
