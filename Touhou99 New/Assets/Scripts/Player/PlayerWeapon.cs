using UnityEngine;
using Mirror;

public class PlayerWeapon : NetworkBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootingPoint;
    [SerializeField] float shootingDelay;

    void Start()
    {
        
    }

    void Update()
    {
		if (isLocalPlayer)
		{
			if (Input.GetKey(KeyCode.Z))
			{
                Shoot();
			}
		}
    }

    void Shoot()
	{
        var newbullet = Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        newbullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000*Time.deltaTime);
        //var newbullet = Instantiate(bullet);
        Destroy(newbullet, 3f);
        //newbullet.GetComponent<Rigidbody2D>().AddForce(100f;
        print("Blam!");
	}
}
