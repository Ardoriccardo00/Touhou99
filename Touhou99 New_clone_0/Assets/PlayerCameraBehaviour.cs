using Mirror;
using UnityEngine;

public class PlayerCameraBehaviour : NetworkBehaviour
{
    Vector3 position;

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
        if (!transform.parent.GetComponent<PlayerIdentity>().isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
    }
}
