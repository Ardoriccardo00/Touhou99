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
