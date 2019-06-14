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
        if (!this.transform.parent.GetComponent<playerMovement>().isLocalPlayer)
        {
            gameObject.GetComponent<Camera>().enabled = false;
            //gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }
}
/*public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    private static bool cameraExists;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    void Awake()
    {
        //DontDestroyOnLoad(transform.gameObject); test

        //if (!cameraExists)
        //{
        //    cameraExists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else { Destroy(gameObject); }

        //theCamera = GetComponent<Camera>();
        //halfHeight = theCamera.orthographicSize;
        //halfWidth = halfHeight * Screen.width / Screen.height;


    }
    void LateUpdate()
    {
        //targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
*/