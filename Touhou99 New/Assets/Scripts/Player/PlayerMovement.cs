using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : NetworkBehaviour
{
	[Header("Setup")]
	Rigidbody2D rb = null;
	Vector2 movement;

	[Header("Proprieties")]
	[SerializeField] float OriginalMoveSpeed = 1f;
	[SerializeField] float focusedSpeed = .5f;
	float currentMoveSpeed;
	bool isfocused = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		currentMoveSpeed = OriginalMoveSpeed;
	}

	private void Update()
	{
		if (isLocalPlayer)
		{
			movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			isfocused = Input.GetKey(KeyCode.LeftShift);
		}
	}

	private void FixedUpdate()
	{
		HandleMovement(movement);
	}

	void HandleMovement(Vector2 direction)
	{
		if (isfocused) currentMoveSpeed = focusedSpeed;
		else currentMoveSpeed = OriginalMoveSpeed;

		rb.MovePosition((Vector2)transform.position + (direction * currentMoveSpeed * Time.deltaTime));
	}

	public float ReturnMoveSpeed()
	{
		return currentMoveSpeed;
	}
}

/*
 * [SyncVar(hook = nameof(OnHolaCountChanged))] //whenever holaCount changes, OnHolaCountChanges gets invoked
int holaCount = 0;
if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
		{
			print("Sending hola to the server");
			Hola();
		}

		if (isLocalPlayer && transform.position.y >= 50)
		{
			TooHigh();
		}

[Command] //From client to server
	void Hola()
	{
		print("received hola from client");
		holaCount++;
		ReplyHola();
	}

	[ClientRpc] //From server to Clients
	void TooHigh()
	{
		print("Too high");
	}

	[TargetRpc] //From server to specific client or to the client that contains the object with the targetRPC script
	void ReplyHola()
	{
		print("Received hola from server");
	}

	void OnHolaCountChanged(int oldCount, int newCount)
	{
		print("old holas: " + oldCount + " new holas: " + newCount);
	}
*/
