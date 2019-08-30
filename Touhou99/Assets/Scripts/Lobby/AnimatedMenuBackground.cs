﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedMenuBackground : MonoBehaviour
{
    Vector3 movementDirection;

    Image image;

    float moveSpeed;
    float rotateSpeed;

    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(255, 255, 255, Random.Range(0.5f, 0.8f));

        rotateSpeed = Random.Range(5f, 10f);
        moveSpeed = Random.Range(5f, 10f);

        movementDirection = new Vector2(Random.Range(-0.3f, 0.3f), -1);
    }

    void Update()
    {
        transform.Translate(movementDirection * moveSpeed, Space.World);
        transform.Rotate(0, 0, 1f * rotateSpeed);
    }
}