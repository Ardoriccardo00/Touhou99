using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBackground : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * 50));
    }
}
