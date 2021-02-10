using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BulletBehaviour : NetworkBehaviour
{
    public Collider2D myCollider;
	public Rigidbody2D rb;
    public PlayerIdentity playerWhoShotMe;
	[SerializeField] float moveSpeed = 1000;
	public float bulletDamage = 10;
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

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			//CmdBulletHitEffect();
			playerWhoShotMe.GetComponent<PlayerWeapon>().CmdDamagePlayer(collision.GetComponent<PlayerIdentity>(), bulletDamage);
			//FindObjectOfType<NetworkGameManager>().RpcDamagePlayer(collision.GetComponent<PlayerIdentity>(), bulletDamage);
			//CmdServerDestroy(); later
		}
	}

	[Command]
	void CmdBulletHitEffect()
	{
		FindObjectOfType<NetworkGameManager>().SaySomething();
	}

	[Command]
	void CmdServerDestroy()
	{
		RpcDestroy();
	}

	[ClientRpc]
	void RpcDestroy()
	{
		Destroy(gameObject);
	}
}
