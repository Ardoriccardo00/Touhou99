using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using Random = UnityEngine.Random;

[Obsolete]
public class Player : NetworkBehaviour {

    [Header("Statistics")]
    [SerializeField] public int maxHealth = 50;
    [SyncVar (hook = "OnHealthChanged")] private int currentHealth;

    public int kills;
    public int deaths;

    [SyncVar] public string username = "loading...";

    [Header("Components")]
    [SerializeField] private Weapon weapon;
    private NetworkManager networkManager;
    PlayerUI[] uiList;

    [Header("Others")]
    [SerializeField] private GameObject Arena;
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";

    private bool isHit;

    void Start()
    {
        if (!isLocalPlayer)
            return;

        Arena = GameObject.FindGameObjectWithTag("Arena");
        isHit = false;
        currentHealth = maxHealth;
        networkManager = NetworkManager.singleton;

        uiList = FindObjectsOfType<PlayerUI>();

        foreach (PlayerUI ui in uiList)
        {
            if (ui.player.transform.name != transform.name)
            {
                ui.gameObject.SetActive(false);
            }
        }
    }


    void Update()
    {
        if (PauseMenu.IsOn)
            return;

        if (this.isLocalPlayer) SendMessage("GetInput");
        else return;
    }

    public float GetHealth()
    {
        return (float)currentHealth / maxHealth;
    }

    public float GetBombPowerAmount()
    {
        return weapon.bombPower;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "Enemy" || hitInfo.tag == "EnemyBullet")
        {
            if (!isLocalPlayer)
                return;

            if (isServer)
                CmdPlayerShot(transform.name, 10);
            else if (isClient)
                ClientPlayerShot(transform.name, 10);
        }
    }

    [Command]
    void CmdPlayerShot(string _playerID, int _damage)
    {
        GameObject go = GameObject.Find(_playerID);
        go.GetComponent<Player>().CmdTakeDamage(_damage);
    }

    [Client]
    void ClientPlayerShot(string _playerID, int _damage)
    {
        GameObject go = GameObject.Find(_playerID);
        go.GetComponent<Player>().ClientTakeDamage(_damage);
    }

    [Command]
    public void CmdEnemyShot(string _enemyID, string _sourceID)
    {
        Debug.Log(_enemyID + " enemy has been shot");

        Destroy(GameObject.Find(_enemyID));
    }

    [Client]
    public void ClientEnemyShot(string _enemyID, string _sourceID)
    {
        Debug.Log(_enemyID + " enemy has been shot");

        Destroy(GameObject.Find(_enemyID));
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
}
