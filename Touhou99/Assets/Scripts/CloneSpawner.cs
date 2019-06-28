using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloneSpawner : NetworkBehaviour
{
    public GameObject clonePrefab;

   public void InstantiateClone()
    {
        GameObject clone = Instantiate(clonePrefab, transform.position, transform.rotation);
        NetworkServer.Spawn(clone);
    }
}
