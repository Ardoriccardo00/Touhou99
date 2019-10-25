using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[Obsolete]
public class Player : NetworkBehaviour {

    [Header("Statistics")]
    [SerializeField] public int maxHealth = 10;
    [SerializeField] [SyncVar (hook = "OnHealthChanged")] private int currentHealth;

    public int kills;
    public int deaths;

    [SyncVar] public string username = "loading...";

    [Header("Components")]
    [SerializeField] private PlayerWeapon weapon;
    private NetworkManager networkManager;

    [Header("Others")]
    //[SerializeField] private GameObject Arena;
    private bool isHit;

    [Header("HealthBar")]
    [SerializeField] private GameObject healthBarObject;
    [SerializeField]
    private Image[] healthbar = new Image[5];

    [SerializeField]
    private Sprite[] pictures = new Sprite[3];

    public Slider bombPowerBar;

    void Start()
    {
        //Arena = GameObject.FindGameObjectWithTag("Arena");
        isHit = false;
        networkManager = NetworkManager.singleton;

        weapon = GetComponent<PlayerWeapon>();
        healthBarObject = GameObject.Find("HealthPanel");
        healthbar = healthBarObject.gameObject.GetComponentsInChildren<Image>();
        bombPowerBar = FindObjectOfType<Slider>();
    }


    void Update()
    {
        if (PauseMenu.IsOn)
            return;

        if (this.isLocalPlayer) SendMessage("GetInput");
        else return;

        SetBombPowerAmount();
        SetHealthAmount();
    }

    public void SetHealth()
    {
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return (int)currentHealth;
    }

    public float GetBombPowerAmount()
    {
        return weapon.bombPower;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (isLocalPlayer && hitInfo.tag == "Enemy" || hitInfo.tag == "EnemyBullet")
        {
            CmdTakeDamage(2);
        }
    }

    [Command]
    public void CmdTakeDamage(int _amount)
    {
        if (!isHit)
        {
            StartCoroutine("HurtColor");
            currentHealth -= _amount;

            Debug.Log(transform.name + " now has " + currentHealth + " health ");

            if (currentHealth <= 0)
            {
                Die();
            }
        }

    }


    //[Command]
    //void CmdPlayerShot(string _playerID, int _damage)
    //{
    //    GameObject go = GameObject.Find(_playerID);
    //    go.GetComponent<Player>().CmdTakeDamage(_damage);
    //}

    //[Client]
    //void ClientPlayerShot(string _playerID, int _damage)
    //{
    //    GameObject go = GameObject.Find(_playerID);
    //    go.GetComponent<Player>().ClientTakeDamage(_damage);
    //}

    //[Command]
    //public void CmdEnemyShot(string _enemyID, string _sourceID)
    //{
    //    Debug.Log(_enemyID + " enemy has been shot");

    //    Destroy(GameObject.Find(_enemyID));
    //}

    //[Client]
    //public void ClientEnemyShot(string _enemyID, string _sourceID)
    //{
    //    Debug.Log(_enemyID + " enemy has been shot");

    //    Destroy(GameObject.Find(_enemyID));
    //}




    [Client]
    public void ClientTakeDamage(int _amount)
    {
        if (!isHit)
        {
            StartCoroutine("HurtColor");
            currentHealth -= _amount;

            Debug.Log(transform.name + " now has " + currentHealth + " health ");

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void OnHealthChanged(int health)
    {
        currentHealth = health;
    }

    IEnumerator HurtColor()
    {
        isHit = true;
        for (int i = 0; i < 5; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.3f); //Red, Green, Blue, Alpha/Transparency
            yield return new WaitForSeconds(.2f);
            GetComponent<SpriteRenderer>().color = Color.white; //White is the default "color" for the sprite, if you're curious.
            yield return new WaitForSeconds(.2f);
        }

        isHit = false;
    } 

    private void Die()
    {
        deaths++;
        Destroy(gameObject);
        GameManager.RemoveDeadPlayer(transform.name);
    }

    public void SetBombPowerAmount()
    {
        bombPowerBar.value = weapon.bombPower;
    }

    void SetHealthAmount()
    {
        switch (currentHealth)
        {
            case 8:
                for (int i = 0; i < 5; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                healthbar[5].sprite = pictures[2];
                break;

            case 6:
                for (int i = 0; i < 4; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                for (int i = 5; i > 3; i--)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;

            case 4:
                for (int i = 0; i < 3; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                for (int i = 5; i > 2; i--)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;

            case 2:
                for (int i = 0; i < 1; i++)
                {
                    healthbar[i].sprite = pictures[0];
                }
                for (int i = 5; i > 0; i--)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;

            case 0:
                for (int i = 0; i < 5; i++)
                {
                    healthbar[i].sprite = pictures[2];
                }
                break;
        }
        if (currentHealth == 10)
        {
            for (int i = 0; i < 6; i++)
            {
                healthbar[i].sprite = pictures[0];
            }
        }

    }
}
