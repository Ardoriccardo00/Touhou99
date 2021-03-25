using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNM : MonoBehaviour
{
	GameObject nm;

	private void OnEnable()
	{
		nm = FindObjectOfType<MyNewtworkManager>().gameObject;
		Destroy(nm);
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
