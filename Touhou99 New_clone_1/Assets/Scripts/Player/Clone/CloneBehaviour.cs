using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CloneBehaviour : NetworkBehaviour
{
    [Header("Setup")]
    Rigidbody2D rb = null;
    [SerializeField] float moveSpeed = 100f;

    [SerializeField] BulletBehaviour bullet;
    [SerializeField] Transform shootingPoint;

    [Header("Timers")]
    [SerializeField] float timerMaxBeforeMoving = 5f;
    float timerBeforeMoving = 0;

    //[SerializeField] float timerMaxToShoot = 5f;
    float timerToShoot = 0;

    [Header("Stats")]
    bool isMoving = false;
    bool canMove = false;

    [SerializeField] Vector3 newPosition;

    [SerializeField] int timesItCanShoot = 1;

    [SerializeField] float shootDelay = .1f;

    void Start()
    {
        timerBeforeMoving = timerMaxBeforeMoving;
        rb = GetComponent<Rigidbody2D>();
        GenerateNewPos();
    }

    void Update()
    {
        if(timerBeforeMoving > 0)
		{
            timerBeforeMoving -= Time.deltaTime;
        }

		else
		{
            if (transform.position != newPosition) //If the position is different from the new position it can move
            {
                canMove = true;
            }

            else canMove = false; //Else it can't move

            if (canMove) //if it can move it goes to the new position
            {
                isMoving = true;
                transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
            }

            else if (!canMove) //if it can't move it must find a new position
            {
                GenerateNewPos();
                //StartCoroutine(StartShootingRoutine());

                /*float newPosX, newPosY;
                newPosX = Random.Range(-6f, 3f);
                newPosY = Random.Range(-6f, 3f);

                newPosX += transform.position.x;
                newPosY += transform.position.y;

                isMoving = false;

                newPosition = new Vector2(newPosX, transform.position.y);*/
                timerBeforeMoving = timerMaxBeforeMoving;

                StartCoroutine(StartShootingRoutine());
            }
        }        
    }

    void GenerateNewPos()
	{
        float newPosX, newPosY;
        newPosX = Random.Range(-6f, 3f);
        newPosY = Random.Range(-6f, 3f);

        newPosX += transform.position.x;
        newPosY += transform.position.y;

        isMoving = false;

        newPosition = new Vector2(newPosX, transform.position.y);
    }

    [Command(ignoreAuthority = true)]
    void CmdShoot()
	{
        var newCloneBullet = Instantiate(bullet, shootingPoint.transform.position, shootingPoint.transform.rotation);
        NetworkServer.Spawn(newCloneBullet.gameObject);
	}

    IEnumerator StartShootingRoutine()
	{
        for(int i = 0; i < timesItCanShoot; i++)
		{
            //CmdShoot();
            yield return new WaitForSeconds(shootDelay);
        }
        yield return null;
	}
}
