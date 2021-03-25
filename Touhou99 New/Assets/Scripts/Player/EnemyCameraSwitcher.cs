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

	//[SerializeField] PlayerWeapon playerWeapon;

	public delegate void OnCameraSwitch(PlayerWeapon ownerOfCameraSwitched);
	public event OnCameraSwitch cameraSwitched;

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

		//AssignRandomEnemyCamera();
		if (!isLocalPlayer) return;
		SwitchEnemyCamera(false);
	}

	public void SwitchEnemyCamera(bool forward) //This function gets called whenever the player presses Q or E to switch the enemy cam to spy
	{
		if (forward) //Forward determines if it should move to the next or previous camera
		{
			if (cameraIndex == cameraList.Count - 1) cameraIndex = 0;
			else cameraIndex++;
		}
		else if (!forward)
		{
			if (cameraIndex == 0) cameraIndex = cameraList.Count - 1;
			else cameraIndex--;
		}

		AssignEnemyCamera(0, false); //Doesn't overwrite so the int can be anything
	}

	private void AssignEnemyCamera(int i, bool canOverwrite)
	{
		if(GameManager.playerDictionary.Count == 1)
		{
			return;
		}

		print("i =" + i);
		DisableAllCameras(); //what it says
		if(enemyPlayerCamera != null) AssignTargetTextureToEnemyCamera(false); //removes the render texture from the active enemy camera

		if (canOverwrite)
		{
			enemyPlayerCamera = cameraList[i]; //swithches enemy camera, by choosing from the list
		}
		else
		{
			enemyPlayerCamera = cameraList[cameraIndex]; //swithches enemy camera, by choosing from the list
		}

		enemyPlayerCamera.gameObject.SetActive(true); //enables the new enemy camera GO 
		cameraSwitched?.Invoke(enemyPlayerCamera.GetComponentInParent<PlayerWeapon>());
		//playerWeapon.SetTargetPlayer(enemyPlayerCamera.GetComponentInParent<PlayerWeapon>()); // the player owner to the camera gets assigned to the player weapon component
		AssignTargetTextureToEnemyCamera(true); //Assigns the render texture to the enemy camera
		print("Camera assigned");
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
