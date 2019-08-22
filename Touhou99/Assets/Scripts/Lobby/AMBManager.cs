using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AMBManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefab;
    private Vector2 spawnPoint;
    GameObject movingSprite;

    int length = 1920;
    int height = 1080;

    private int posX;
    private int posY;

    float maxTimeDown = 0.05f;
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
        movingSprite = Instantiate(prefab[Random.Range(0,prefab.Length)], spawnPoint, Quaternion.identity);
        movingSprite.transform.SetParent(transform);
        Destroy(movingSprite, timeToLive);
        //StartCoroutine(FadeAwayCounter(timeToLive));
    }

    IEnumerator FadeAwayCounter(float timer)
    {
        //print(timer);
        while (timer > 0)
        {
            timer -= Time.deltaTime / 2;
        }
       
        if (timer <= 0)
        {
            Image movingSpriteImage = movingSprite.GetComponent<Image>();
            Color newColor = new Color(255, 255, 255, movingSpriteImage.color.a);

            while (movingSpriteImage.color.a > 0)
            {
                movingSpriteImage.color = newColor;
            }
        }
        yield return null;
    }
}
