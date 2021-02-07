using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 position;

    void Awake()
    {
        position = transform.position;
    }
    void LateUpdate()
    {
        transform.position = position;
    }

    [System.Obsolete]
    void Update()
    {
        if (!this.transform.parent.GetComponent<Player>().isLocalPlayer)
        {
            gameObject.GetComponent<Camera>().enabled = false;
        }
    }
}