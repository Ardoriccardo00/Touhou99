using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

[Obsolete]
public class playerMovement : NetworkBehaviour {
    [Header("Official")]

    [SerializeField]
    private int maxHealth = 50;

    [SyncVar]
    private int currentHealth;

    public int GetHealth()
    {
        return currentHealth;
    }

    [SyncVar]
    public string username = "loading...";

    public int kills;
    public int deaths;

    private Rigidbody2D rb;
    public float bombPower;
    private float originalMoveSpeed;

    [SerializeField]
    private float moveSpeed;
    private Vector2 direction;

    private Camera myCamera;

    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";

    [SerializeField]
    private Animator animator;

    private NetworkManager networkManager;

    [Header("Weapon")]
    public Transform firePoint;
    public Transform firePoint1;
    public Transform bombFirePoint;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;

    public float distance = 100f;

    public float fireRate = 0f;

    public int damage = 10;

    public void Awake()
    {
        SetDefaults();
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        //health = maxHealth;
        originalMoveSpeed = moveSpeed;
        bombPower = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        networkManager = NetworkManager.singleton;
    }
    void Update()
    {
        if (PauseMenu.IsOn)
            return;

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
            //animator.SetFloat("direction", 0.2f);
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

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));

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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Destroy(bullet, 0.5f);
        Destroy(bullet1, 0.5f);
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage, string _sourceID)
    {
        Debug.Log(_playerID + " has been shot");

        playerMovement _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(_damage, _sourceID);
    }

    [Command]
    void CmdEnemyShot(string _enemyID, int _damage, string _sourceID)
    {
        Debug.Log(_enemyID + " enemy has been shot");

        Destroy(GameObject.Find(_enemyID));
    }

    
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            RpcTakeDamage(10, "Enemy hit");
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);
    }

    [ClientRpc]
    public void RpcTakeDamage(int _amount, string _sourceID)
    {
        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health ");

        if (currentHealth <= 0)
        {
            Die(_sourceID);
        }
    }

    private void Die(string _sourceID)
    {
        //playerMovement sourcePlayer = GameManager.GetPlayer(_sourceID);
        //if (sourcePlayer != null)
        //{
        //    sourcePlayer.kills++;
        //    GameManager.instance.onPlayerKilledCallBack.Invoke(username, sourcePlayer.username);
        //}

        deaths++;
        //Instantiate(deathEffect, transform.position, Quaternion.identity); //Ripristinare quando verra' aggiunta un'animazione di morte
        Destroy(gameObject);
        GameManager.RemoveDeadPlayer(transform.name);
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
/*void Shoot()
    {
        //Debug.Log("shoot");
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bullet1 = Instantiate(bulletPrefab, firePoint1.position, firePoint1.rotation);
        Destroy(bullet, 3f);
        Destroy(bullet1, 3f);
        //bullet.transform.Rotate(new Vector3(0, 0, -90));


        //Debug.DrawLine(firePoint.position, firePoint.position + firePoint.up);
        //RaycastHit2D hit = Physics2D.Raycast(firePoint.position, firePoint.position + firePoint.up * distance, Mathf.Infinity);
        //Debug.DrawLine(firePoint1.position, firePoint1.position + firePoint1.up);
        //RaycastHit2D hit1 = Physics2D.Raycast(firePoint1.position, firePoint1.position + firePoint1.up * distance, Mathf.Infinity);

        //if (hit.collider != null || hit1.collider != null)
        //{
        //    Debug.Log(hit.collider.name);
        //}
        //if (hit.collider.tag == PLAYER_TAG || hit1.collider.tag == PLAYER_TAG)
        //{
        //    CmdPlayerShot(hit.collider.name, damage, transform.name);
        //    CmdPlayerShot(hit1.collider.name, damage, transform.name);
        //}

        //if (hit.collider.tag == ENEMY_TAG || hit1.collider.tag == ENEMY_TAG)
        //{
        //    CmdEnemyShot(hit.collider.name, damage, transform.name);
        //    CmdEnemyShot(hit1.collider.name, damage, transform.name);
        //}
    }*/
