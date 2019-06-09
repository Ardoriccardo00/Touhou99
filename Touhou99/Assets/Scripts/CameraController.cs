using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;
    private static bool cameraExists;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);

        //if (!cameraExists)
        //{
        //    cameraExists = true;
        //    DontDestroyOnLoad(transform.gameObject);
        //}
        //else { Destroy(gameObject); }

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }
    void Update()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
