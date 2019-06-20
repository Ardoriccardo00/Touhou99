using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject save;

    private void Awake()
    {
        DontDestroyOnLoad(save);
    }
}
