using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

[System.Obsolete]
public class Weapon : NetworkBehaviour
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
    private GameObject projectilesContainer;
    private GameObject clonesContainer;

    public GameObject bulletPrefab;
    public GameObject bombPrefab;
    public GameObject clonePrefab;

    private void Start()
    {
        bombPower = 0f;
        cloneSpawnPoint = GameObject.FindGameObjectWithTag("CloneSpawner");
        projectilesContainer = GameObject.FindGameObjectWithTag("ProjectilesContainer");
        clonesContainer = GameObject.FindGameObjectWithTag("ClonesContainer");
    }

    private void Update()
    {
        if (bombPower >= 40f)
            bombPower = bombPowerMax;
    }

    void Bomb()
    {
        if (!isLocalPlayer)
            return;

        GameObject bomb = Instantiate(bombPrefab, bombFirePoint.position, bombFirePoint.rotation);
        NetworkServer.Spawn(bomb);
        bomb.transform.SetParent(projectilesContainer.transform);

        GameObject clone = Instantiate(clonePrefab, cloneSpawnPoint.transform.position, cloneSpawnPoint.transform.rotation);
        NetworkServer.Spawn(clone);
        clone.transform.SetParent(clonesContainer.transform);
    }

    void Shoot()
    {
        if (!isLocalPlayer)
            return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        NetworkServer.Spawn(bullet);
        bullet.transform.SetParent(projectilesContainer.transform);
        bullet.GetComponent<ProjectileBehaviour>().shooter = transform.name;
        Destroy(bullet, 1f);

    }
}
