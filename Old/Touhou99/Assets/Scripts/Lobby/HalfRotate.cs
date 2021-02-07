using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfRotate : MonoBehaviour
{
    [SerializeField] float rotatingTime = 1f;
    [SerializeField] private float rotatingSpeed = 400f;

    float rotatingTimeTimer;
    void Start()
    {
        rotatingTimeTimer = rotatingTime;
    }

    void Update()
    {
        if (Mathf.Round(transform.eulerAngles.z) >= 180)
        {
            transform.Rotate(new Vector3(0, 0, -180 * Time.deltaTime));
        }
        else if (Mathf.Round(transform.eulerAngles.z) <= -180)
        {
            transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime));
        }

        else
            transform.Rotate(new Vector3(0, 0, 180 * Time.deltaTime));
    }
}
