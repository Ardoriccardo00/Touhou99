using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMBManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] private Transform spawnPoint;
    void Start()
    {
        
    }

    void Update()
    {
        GameObject movingSprite = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
