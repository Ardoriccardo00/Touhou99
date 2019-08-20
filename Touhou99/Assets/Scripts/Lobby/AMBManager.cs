using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AMBManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefab;
    private Vector2 spawnPoint;

    int length = 1920;
    int height = 1080;

    private int posX;
    private int posY;

    float maxTimeDown = 0.1f;
    float timeDown;

    float timeToLive;

    void Start()
    {
        
    }

    void Update()
    {
        posX = Random.Range(0, length);
        posY = Random.Range(0, height);

        spawnPoint = new Vector2(posX, posY);

        timeToLive = Random.Range(1f, 2f);

        if(timeDown > 0f)
        {
            timeDown-= Time.deltaTime;
        }
        else
        {
            Spawn();
            timeDown = maxTimeDown;
        }
    }

    void Spawn()
    {
        GameObject movingSprite = Instantiate(prefab[Random.Range(0,prefab.Length)], spawnPoint, Quaternion.identity);
        movingSprite.transform.SetParent(transform);
        Destroy(movingSprite, timeToLive);
    }
}
