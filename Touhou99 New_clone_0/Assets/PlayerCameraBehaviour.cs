using Mirror;
using UnityEngine;

public class PlayerCameraBehaviour : NetworkBehaviour
{
    Vector3 position;
    public bool stayFixed = true;

    void Awake()
    {
        position = transform.position;
    }
    void LateUpdate()
    {
        if (stayFixed) transform.position = new Vector3(position.x, position.y, -.5f);
        else position = FindObjectOfType<PlayerIdentity>().transform.position;
    }

    void Update()
    {
        if (!transform.parent.GetComponent<PlayerIdentity>().isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
    }
}
