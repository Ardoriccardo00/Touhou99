using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float moveSpeed;
    private float originalMoveSpeed;
    private float currentMoveSpeed;
    public float diagonalMoveModifier = 0.75f;
    private Rigidbody2D rb;
    private Camera myCamera;
    private Weapon weapon;
    [SerializeField] private GameObject hitBoxSprite;
    [SerializeField] private Animator animator;
    void Start()
    {
        originalMoveSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        hitBoxSprite.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        
    }

    private void GetInput()
    {
        Move();
        AdjustDiagonalMovement();
        Focus();
        GetWeaponInput();

        if (Input.GetKeyDown(KeyCode.H))
        {
            weapon.bombPower = weapon.bombPowerMax;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetTarget();
        }
    }

    private void GetWeaponInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SendMessage("Shoot");
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            if (weapon.bombPower >= 40f)
            {
                SendMessage("Bomb");
                weapon.bombPower = weapon.bombPower - 40f;
            }
            else { Debug.Log("no"); }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CloneMovement cm = GameObject.FindObjectOfType<CloneMovement>();
            cm.CloneAttack();
        }
    }

    private void AdjustDiagonalMovement()
    {
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f && Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
        {
            currentMoveSpeed = moveSpeed * diagonalMoveModifier;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }
    }

    private void Focus()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            hitBoxSprite.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            currentMoveSpeed = originalMoveSpeed / 2;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            hitBoxSprite.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            currentMoveSpeed = originalMoveSpeed;
        }
    }

    private void Move()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * currentMoveSpeed, rb.velocity.y);

        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * currentMoveSpeed);

        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
            rb.velocity = new Vector2(0f, rb.velocity.y);

        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            rb.velocity = new Vector2(rb.velocity.x, 0f);

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
    }

    void SetTarget()
    {
        if (!isLocalPlayer)
            return;

        GameObject[] cloneSpawnPoints = GameObject.FindGameObjectsWithTag("CloneSpawner");
        int random = Random.Range(0, cloneSpawnPoints.Length);
        GameObject targetSpawn = cloneSpawnPoints[random];
        print("Giocatore besagliato: " + targetSpawn);

        //playerMovement[] playerList = FindObjectsOfType<playerMovement>();
        //int intTargetPlayer = Random.Range(0, playerList.Length);
        //playerMovement targetPlayer = playerList[intTargetPlayer];
        //print("Giocatore besagliato: " + targetPlayer);
    }
}
