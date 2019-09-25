using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class CloneMovement : NetworkBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 1000;
    public int currentHealth;

    [Header("Movement")]
    private float timeBetweenMove = 2f;
    private float timeBetweenMoveCounter;

    private float timeToMove = 0.5f;
    private float timeToMoveCounter;

    private bool moving;
    private Vector3 moveDirection;
    float horizontal;
    private bool facingRight = true;


    [SerializeField] private int moveSpeed = 5;

    [Header("Components")]
    private Rigidbody2D rb;
    private Transform target;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.5f, timeBetweenMove * 1f);

        target = GameObject.FindGameObjectWithTag("CloneSpawner").GetComponent<Transform>();

    }

    [System.Obsolete]
    void Update()
    {
        horizontal = moveDirection.x;

        Flip(horizontal);

        if (moving)
        {
            MoveClone();

            if (timeToMoveCounter < 0f)
            {
                StopClone();

            }
        }
        else
        {
            CountDownBetweenMovements();

            if (timeBetweenMoveCounter < 0f)
            {
                ResetMovement();
            }
        }

        if (currentHealth <= 0)
            NetworkServer.Destroy(gameObject);
    }

    private void ResetMovement()
    {
        moving = true;
        timeToMoveCounter = Random.Range(timeToMove * 0.5f, timeBetweenMove * 1f);

        moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(0f, 0f) * moveSpeed, 0f);
    }

    private void CountDownBetweenMovements()
    {
        animator.SetFloat("Speed", 0f);
        timeBetweenMoveCounter -= Time.deltaTime;
        rb.velocity = Vector2.zero;
    }

    private void StopClone()
    {
        moving = false;
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
    }

    private void MoveClone()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveDirection.x));
        timeToMoveCounter -= Time.deltaTime;
        rb.velocity = moveDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Arena")
        {
            moveDirection.x = -moveDirection.x;
        }
    }

    public void CloneAttack()
    {
        for(float i = 0f; i < 10f; i++)
        {
            animator.SetBool("isAttacking", true);
        }
        animator.SetBool("isAttacking", false);
    }

    private void Flip(float flip)
    {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;

            scale.x *= -1;

            transform.localScale = scale;
        }
    }
}