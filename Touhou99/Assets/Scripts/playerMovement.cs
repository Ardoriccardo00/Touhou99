using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Obsolete]
public class playerMovement : NetworkBehaviour {
    //Creare uno script per il network manager in modo che instanzi le 50 arene con id assegnato
    [Header("Official")]
    [SerializeField]
    private int maxHealth = 500;

    [SyncVar]
    private int health;

    [SyncVar]
    public int kills;

    [SyncVar]
    public int deaths;

    private Rigidbody2D rb;
    public float bombPower;
    private float originalMoveSpeed;

    [SerializeField]
    private float moveSpeed;
    private Vector2 direction;

    private Camera myCamera;

    private const string PLAYER_TAG = "Player";

    [Header("Weapon")]
    public Transform firePoint;
    public Transform firePoint1;
    public Transform bombFirePoint;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    public float distance = 100f;

    public float fireRate = 0f;

    public void Setup()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        health = maxHealth;
    }

    void Start()
    {
        //health = maxHealth;
        originalMoveSpeed = moveSpeed;
        bombPower = 0;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (this.isLocalPlayer)
        {
            GetInput();
        }
    }
    private void GetInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
        }

        if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 4;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = originalMoveSpeed;
        }

        //weapon
        if (fireRate <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / fireRate);
            }
            else if (Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (bombPower >= 40f)
            {
                Bomb();
                bombPower = bombPower - 40f;
            }
            else { Debug.Log("no"); }
        }
    }

    [Client]
    void Bomb()
    {
        Instantiate(bombPrefab, bombFirePoint.position, bombFirePoint.rotation);
    }

    [Client]
    void Shoot()
    {
        //Debug.Log("shoot");
        //GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //GameObject bullet2 =  Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
        Debug.DrawLine(transform.position, transform.position + transform.up);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position + transform.up * distance, Mathf.Infinity);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
        if (hit.collider.tag == PLAYER_TAG)
        {
            CmdPlayerShot(hit.collider.name);
        }

        //if (hit.collider.tag == "Enemy")
        //{
        //    CmdEnemyShot(hit.collider.name);
        //}
    }

    [Command]
    void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + " has been shot");
    }

    [Command]
    void CmdEnemyShot(string _ID)
    {
        Debug.Log(_ID + " enemy has been shot");
    }

    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(100);
            TakeDamage(100);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
    }

    //[ClientRpc]
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
        Destroy(gameObject);
    }
}

//public int health;

//[SerializeField]
//private float moveSpeed;
//private Vector2 direction;
//// Update is called once per frame
//void Update()
//{

//}

//void FixedUpdate()
//{
//    //GetInput();
//    //Move();

//    if (this.isLocalPlayer)
//    {
//        GetInput();
//        Move();
//    }
//}

//public void Move()
//{
//    transform.Translate(direction * moveSpeed * Time.deltaTime);
//}

//private void GetInput()
//{
//    direction = Vector2.zero;

//    if (Input.GetKey(KeyCode.UpArrow))
//    {
//        direction += Vector2.up;
//    }
//    if (Input.GetKey(KeyCode.LeftArrow))
//    {
//        direction += Vector2.left;
//    }
//    if (Input.GetKey(KeyCode.DownArrow))
//    {
//        direction += Vector2.down;
//    }
//    if (Input.GetKey(KeyCode.RightArrow))
//    {
//        direction += Vector2.right;
//    }
//}

//private void OnTriggerEnter2D(Collider2D hitInfo)
//{
//    Enemy enemy = hitInfo.GetComponent<Enemy>();
//    if (enemy != null)
//    {
//        enemy.TakeDamage(100);
//        TakeDamage(100);
//    }
//    //Instantiate(impactEffect, transform.position, transform.rotation);
//}

//public void TakeDamage(int damage)
//{
//    health -= damage;

//    if (health <= 0)
//    {
//        Die();
//    }
//}

//void Die()
//{
//    //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
//    Destroy(gameObject);
//}
//}

//if(Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) {
//    transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
//}
//if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
//{
//    transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
//}

//if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
//{
//    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
//}
//if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
//{
//    rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
//}

//if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f)
//{
//    rb.velocity = new Vector2(0f, rb.velocity.y);
//}
//if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
//{
//    rb.velocity = new Vector2(rb.velocity.x, 0f);
//}

//public override void OnStartLocalPlayer()
//{
//    //myCamera = Instantiate(Camera.main);
//    //myCamera.transform.rotation = transform.rotation;
//    // myCamera.transform.position = transform.position + new Vector3(transform.position.x, transform.position.y, transform.position.z);
//    //myCamera.transform.SetParent(transform);
//}
/*[SerializeField]
private Behaviour[] disableOnDeath;
private bool[] wasEnabled;*/
/*    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }*/
/*    public void SetDefaults()
    {
        isDead = false;
        health = maxHealth;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = true;
    }*/
/*    public void Setup()
{
    wasEnabled = new bool[disableOnDeath.Length];

    for (int i = 0; i < wasEnabled.Length; i++)
    {
        wasEnabled[i] = disableOnDeath[i].enabled;
    }
    SetDefaults();
}*/
/*   public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }*/
/*private void Die()
    {
        //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
        isDead = true;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider _col = GetComponent<Collider>();
        if (_col != null)
            _col.enabled = false;

        Debug.Log(transform.name + "is dead");
    }
*/
//weapon
/*if (Input.GetKeyDown(KeyCode.Z))
        {
            if (this.isLocalPlayer)
            {
                Shoot();
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.bombPower >= 40f)
            {
                Bomb();
                player.bombPower = player.bombPower - 40f;
            }
            else { Debug.Log("no"); }
        }
*/
/*    [Command]
    void CmdEnemyShot(string _ID)
    {
        Debug.Log(_ID + " has been shot");
    }

    [Command]
    void CmdPlayerShot(string _ID)
    {
        Debug.Log(_ID + " enemy has been shot");
    }
*/
