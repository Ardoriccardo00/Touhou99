using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedMenuBackground : MonoBehaviour
{
    [SerializeField] float speed = 100f;

    float moveSpeed;
    enum Direction { Up, Down, Left, Right };

    [SerializeField] Direction moveDirection;


    void Start()
    {
        moveSpeed = speed * Time.deltaTime;
    }

    void Update()
    {
        print("ciao");

        switch (moveDirection)
        {
            case Direction.Up:
                transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
                break;

            case Direction.Down:
                transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
                break;

            case Direction.Left:
                transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                break;

            case Direction.Right:
                transform.Translate(Vector3.right * moveSpeed);
                break;
        }

        if (transform.position.x >= 2000)
        {
            this.transform.localPosition = Vector3.zero;
        }
    }
}
