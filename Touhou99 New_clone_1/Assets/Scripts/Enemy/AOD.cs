using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class AOD : NetworkBehaviour
{
    void Start()
    {
        float newScale = Random.Range(3f, 5f);
        transform.localScale = new Vector3(newScale, newScale, newScale);

        StartCoroutine(DestroyThis());
    }

    IEnumerator DestroyThis()
	{
        yield return new WaitForSeconds(4);
        NetworkServer.Destroy(gameObject);
        Destroy(gameObject);
    }
}
