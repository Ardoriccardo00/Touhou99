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
        thisCloneSpawnPoint = FindObjectOfType<CloneSpawnPoint>().gameObject;
        print("THIS CLONE SP" + thisCloneSpawnPoint);
        SetTarget();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;           

        if (bombPower >= 40f)
            bombPower = bombPowerMax;

        if(cloneSpawnPoint == thisCloneSpawnPoint)
        {
            SetTarget();
            print("reset target");
        }
    }

    public void SetBombPower()
    {
        bombPower = 0f;
    }

    void Shoot()
    {
        CmdSpawnBullet();
    }

    void Bomb()
    {
        CmdCreateBomb();     
    }

    private void CreateClone()
    {
        CmdSpawnClone();
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.SetParent(projectilesContainer.transform);
        bullet.GetComponent<ProjectileBehaviour>().shooter = transform.name;
        Destroy(bullet, 1f);
        NetworkServer.Spawn(bullet);
    }

    [Command]
    void CmdCreateBomb()
    {
        GameObject bomb = Instantiate(bombPrefab, bombFirePoint.position, bombFirePoint.rotation);
        bomb.transform.SetParent(projectilesContainer.transform);
        CreateClone();
        NetworkServer.Spawn(bomb);   
    }

    [Command]
    void CmdSpawnClone()
    {   
        if (cloneSpawnPoint.transform.position != thisCloneSpawnPoint.transform.position)
        {
            GameObject clone = Instantiate(clonePrefab, cloneSpawnPoint.transform.position, cloneSpawnPoint.transform.rotation);
            clone.transform.SetParent(clonesContainer.transform);
            NetworkServer.Spawn(clone);
            print("Spawnando clone su " + cloneSpawnPoint);
        }
        else print("Could not spawn clone");     
    }

    [ClientRpc]
    void RpcSpawnBullet(GameObject bulletToSpawn)
    {
        NetworkServer.Spawn(bulletToSpawn);
    }

    [ClientRpc]
    void RpcCreateBomb(GameObject bombToCreate)
    {
        NetworkServer.Spawn(bombToCreate);
    }

    [ClientRpc]
    void RpcSpawnClone(GameObject cloneToSpawn)
    {
        NetworkServer.Spawn(cloneToSpawn);
    }

    void SetTarget()
    {
        GameObject[] cloneSpawnPoints = GameObject.FindGameObjectsWithTag("CloneSpawner");
        int random = Random.Range(0, cloneSpawnPoints.Length);
        cloneSpawnPoint = cloneSpawnPoints[random];
        //GameObject cloneSP = cloneSpawnPoints[random];
        //CreateClone();
        print("Giocatore besagliato: " + cloneSpawnPoint);
        print("Mio spawn point: " + thisCloneSpawnPoint);
    }
}

/*[ClientRpc]
    void RpcShoot()
    {
        CreateBullet();
    }*/
/*[ClientRpc]
    void RpcBomb()
    {
        CreateBomb();
    }*/
