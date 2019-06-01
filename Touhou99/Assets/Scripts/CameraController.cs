using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : MonoBehaviour
{
    private int moveSpeed = 0;
    void Update()
    {
         transform.position = Vector3.Lerp(transform.position, transform.position, moveSpeed * Time.deltaTime );   
    }
}
