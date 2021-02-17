using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class EnemyCameraSwitcher : NetworkBehaviour
{
    [SerializeField] Camera thisPlayerCamera;
	[SerializeField] Camera enemyPlayerCamera;
	List<Camera> cameraList = new List<Camera>();
	[SerializeField] int cameraIndex = 0;
	[SerializeField] RawImage rawImage;
	[SerializeField] RenderTexture renderTexture;

	private void Awake()
	{
		rawImage = FindObjectOfType<RawImage>();
	}

	private void Update()
	{
		if (!isLocalPlayer) return;

		if (Input.GetKeyDown(KeyCode.Q)) SwitchEnemyCamera(false);
		if (Input.GetKeyDown(KeyCode.E)) SwitchEnemyCamera(true);
	}

	public void CreateCameraList()
	{
		Camera[] cameraArray = FindObjectsOfType<Camera>();
		foreach(Camera cam in cameraArray)
		{
			cameraList.Add(cam);
		}

		cameraList.Remove(thisPlayerCamera);

		AssignEnemyCamera();
		//DisableAllCameras();
	}

	void SwitchEnemyCamera(bool forward)
	{
		if (forward)
		{
			if (cameraIndex == cameraList.Count - 1) cameraIndex = 0;
			else cameraIndex++;
		}
		else if (!forward)
		{
			if (cameraIndex == 0) cameraIndex = cameraList.Count - 1;
			else cameraIndex--;
		}

		DisableAllCameras();
		AssignTargetTextureToEnemyCamera(false);
		enemyPlayerCamera = cameraList[cameraIndex];
		enemyPlayerCamera.gameObject.SetActive(true);
		AssignTargetTextureToEnemyCamera(true);
	}

	void AssignEnemyCamera()
	{
		if (cameraList.Count == 0) return;
		var rand = Random.Range(1, cameraList.Count - 1);
		print("rand " + rand);
		enemyPlayerCamera = cameraList[rand - 1];
	}

	void AssignTargetTextureToEnemyCamera(bool assign)
	{
		if (assign) enemyPlayerCamera.targetTexture = renderTexture;
		else enemyPlayerCamera.targetTexture = null;
	}

	void DisableAllCameras()
	{
		foreach(Camera cam in cameraList)
		{
			cam.gameObject.SetActive(false);
		}
	}
}
