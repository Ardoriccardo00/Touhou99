using UnityEngine;
using Mirror;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;

    [SerializeField] float shootingDelay = 1f;
    float shootingDelayTimer = 0;

    void Start()
    {
        shootingDelayTimer = shootingDelay;
    }

    void Update()
    {
		if (isLocalPlayer)
		{
			if (Input.GetKey(KeyCode.Z))
			{
                //print(shootingDelayTimer);
                shootingDelayTimer -= Time.deltaTime;
                if (shootingDelayTimer > 0) return;
				else
				{
                    CmdShoot();
                    shootingDelayTimer = shootingDelay;
                }
			}
		}
    }

    [Command]
    void CmdShoot()
	{
		//print("Client: Asking to shoot");
		RpcShoot();
	}

    [ClientRpc]
    void RpcShoot()
	{
        //print("Server: Shooting");
        var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        Destroy(newbullet, 3f);
    }
}
