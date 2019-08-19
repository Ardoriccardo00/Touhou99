using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlImageRandomizer : MonoBehaviour
{
    Image imageComponent;

    [SerializeField] private Sprite[] spritesList;
    [SerializeField] private int spriteSelected;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        spriteSelected = Random.Range(0, spritesList.Length);
        imageComponent.sprite = spritesList[spriteSelected];
    }

    void Update()
    {
        
    }
}
