using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[System.Obsolete]
public class PlayerWeapon : NetworkBehaviour
{
    [Header("Weapon")]
    public float distance = 100f;
    public float fireRate = 0f;
    public int damage = 10;
    [SyncVar] public float bombPower;
    public float bombPowerMax = 40f;

    [Header("Components")]
    public Transform firePoint;
    public Transform firePoint1;
    public Transform bombFirePoint;
    [SerializeField] GameObject cloneSpawnPoint;
    private GameObject thisCloneSpawnPoint;
    private GameObject projectilesContainer;
    private GameObject clonesContainer;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public GameObject clonePrefab;

    private void Start()
    {
        cloneSpawnPoint = GameObject.FindGameObjectWithTag("CloneSpawner");
        projectilesContainer = GameObject.FindGameObjectWithTag("ProjectilesContainer");
        clonesContainer = GameObject.FindGameObjectWithTag("ClonesContainer");
        thisCloneSpawnPoint = FindObjectOfType<Arena>().gameObject;
        thisCloneSpawnPoint.transform.Find("CloneSpawnPoint");
        print("THIS CLONE SP" + thisCloneSpawnPoint);
        SetTarget();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;           

        if (bombPower >= 40f)
            bombPower = bombPowerMax;
    }

    public void SetBombPower()
    {
        bombPower = 0f;
    }

    [Command]
    void CmdShoot()
    {
        RpcCreateBullet();
    }

    [ClientRpc]
    void RpcCreateBullet()
    {
        CreateBullet();
    }

    void CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
        bullet.transform.SetParent(projectilesContainer.transform);
        bullet.GetComponent<ProjectileBehaviour>().shooter = transform.name;
        Destroy(bullet, 1f);
    }

    [Command]
    void CmdBomb()
    {
        RpcBomb();     
    }

    [ClientRpc]
    void RpcBomb()
    {
        CreateBomb();
    }

    void CreateBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, bombFirePoint.position, bombFirePoint.rotation);
        NetworkServer.Spawn(bomb);
        bomb.transform.SetParent(projectilesContainer.transform);
        SpawnClone();
    }

    private void SpawnClone()
    {
        if (cloneSpawnPoint.transform.position == thisCloneSpawnPoint.transform.position)
        {
            GameObject clone = Instantiate(clonePrefab, cloneSpawnPoint.transform.position, cloneSpawnPoint.transform.rotation);
            NetworkServer.Spawn(clone);
            clone.transform.SetParent(clonesContainer.transform);
        }
    }


    void SetTarget()
    {
        GameObject[] cloneSpawnPoints = GameObject.FindGameObjectsWithTag("CloneSpawner");
        int random = Random.Range(0, cloneSpawnPoints.Length);
        cloneSpawnPoint = cloneSpawnPoints[random];
        print("Giocatore besagliato: " + cloneSpawnPoint);
    }
}
