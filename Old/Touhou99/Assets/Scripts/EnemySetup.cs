using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class EnemySetup : NetworkBehaviour
{
    void Start()
    {
        RegisterEnemy();
    }

    void RegisterEnemy()
    {
        string _ID = "Enemy " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }
}
