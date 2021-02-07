using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotatingSpeed = -400f;
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotatingSpeed);
    }
}
