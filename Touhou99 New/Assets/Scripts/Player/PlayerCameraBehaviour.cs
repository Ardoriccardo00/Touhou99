using Mirror;
using UnityEngine;

public class PlayerCameraBehaviour : NetworkBehaviour
{
    Vector3 position;
    public bool canDisableOthers = false;
    Camera cameraComponent;

	private void OnEnable()
	{
        cameraComponent = GetComponent<Camera>();
	}

	public void SetPosition(Vector3 newPos)
	{
        position = newPos;
	}

	void LateUpdate()
    {
        transform.position = new Vector3(position.x, position.y, -.5f);
    }

    void Update()
    {
		if (transform.parent.GetComponent<PlayerIdentity>().isLocalPlayer)
		{
            cameraComponent.depth = 0;
		}
		else
		{
            GetComponent<AudioListener>().enabled = false;
		}
        /*if (!transform.parent.GetComponent<PlayerIdentity>().isLocalPlayer && canDisableOthers)
        {
            gameObject.SetActive(false);
        }*/
    }
}
