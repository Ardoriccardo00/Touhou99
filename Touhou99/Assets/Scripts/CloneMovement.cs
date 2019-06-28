using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloneMovement : NetworkBehaviour
{
    private int maxHealth = 5000;
    public int currentHealth;

    private float timeBetweenMove = 2f;
    private float timeBetweenMoveCounter;

    private float timeToMove = 0.5f;
    private float timeToMoveCounter;

    private bool moving;

    private Vector3 moveDirection;

    private Rigidbody2D rb;

    [SerializeField]
    private int moveSpeed = 5;

    private Transform target;

    public Animator animator;

    private bool facingRight = true;

    float horizontal;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;

        //timeBetweenMove = UnityEngine.Random.Range(1f, 3f);
        //timeBetweenMoveCounter = timeBetweenMove;
        timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
        timeToMoveCounter = Random.Range(timeToMove * 0.5f, timeBetweenMove * 1f);

        //timeToMoveCounter = timeToMove;

        target = GameObject.FindGameObjectWithTag("CloneSpawner").GetComponent<Transform>();

    }

    void Update()
    {
        horizontal = moveDirection.x;

        Flip(horizontal);

        if (moving)
        {
            animator.SetFloat("Speed",Mathf.Abs(moveDirection.x));
            timeToMoveCounter -= Time.deltaTime;
            rb.velocity = moveDirection;

            if (timeToMoveCounter < 0f)
            {
                moving = false;
                //timeBetweenMoveCounter = timeBetweenMove;
                timeBetweenMoveCounter = Random.Range(timeBetweenMove * 0.75f, timeBetweenMove * 1.25f);
                Debug.Log(timeBetweenMoveCounter);

            }
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            timeBetweenMoveCounter -= Time.deltaTime;
            rb.velocity = Vector2.zero;

            if (timeBetweenMoveCounter < 0f)
            {
                moving = true;
                //timeToMoveCounter = timeToMove;
                timeToMoveCounter = Random.Range(timeToMove * 0.5f, timeBetweenMove * 1f);

                moveDirection = new Vector3(Random.Range(-1f, 1f) * moveSpeed, Random.Range(0f, 0f) * moveSpeed, 0f);
            }
        }

        if (currentHealth <= 0)
            NetworkServer.Destroy(gameObject);
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
//if (rightDirection == true)
//{
//    rightDirection = false;
//    Debug.Log("Destra falsa");
//}

//else
//{
//    rightDirection = true;
//    Debug.Log("Destra vera");
//}

/*if (timeBetweenMoveCounter > 0)//Se il timer tra i movimenti e' maggiore di zero
    {
        timeBetweenMoveCounter -= Time.deltaTime;
        Debug.Log("tempo tra i movimenti: " + timeBetweenMoveCounter);
    }
   else if (timeBetweenMoveCounter <= 0)
    {
        //timeBetweenMoveCounter = timeBetweenMove;
        if (timeToMoveCounter > 0)
        {
            timeToMoveCounter -= Time.deltaTime;
            rb.velocity = new Vector2(1f * moveSpeed, rb.velocity.y);
        }      

        else if (timeToMoveCounter <= 0)
        {
            rb.velocity = Vector2.zero;
            Debug.Log("stop");
            //transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            timeBetweenMove = UnityEngine.Random.Range(1f, 3f);
            timeBetweenMoveCounter = timeToMove;
            timeToMoveCounter = timeToMove;

        }
    }
*/