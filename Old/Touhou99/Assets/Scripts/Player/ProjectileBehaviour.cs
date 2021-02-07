using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[System.Obsolete]
public class ProjectileBehaviour : NetworkBehaviour
{
    public float speed;
    public int damage;
    public string shooter;
    GameObject playerToReward;

    void Start()
    {
        playerToReward = GameObject.Find(shooter);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        if (hitInfo.tag == "Enemy" || hitInfo.tag == "Clone")
        {
            RewardPlayer();

            if (hitInfo.tag == "Enemy")
            {
                DestroyEnemy(hitInfo.gameObject);
            }

            else if (hitInfo.tag == "Clone")
            {
                DamageClone();
            }
        }

        if (hitInfo.tag == "EnemyBullet")
        {
            Destroy(hitInfo.gameObject);
            NetworkServer.Destroy(hitInfo.gameObject);
        }
           NetworkServer.Destroy(gameObject);
           Destroy(gameObject);
    }

    private void DestroyEnemy(GameObject hitInfo)
    {
        var varToPass = hitInfo;
        CmdDestroyEnemy(varToPass);
    }

    private void RewardPlayer()
    {
        CmdRewardPlayer();
    }

    private void DamageClone()
    {
        CmdDamageClone();
    }

    [Command]
    void CmdRewardPlayer()
    {
        playerToReward.GetComponent<PlayerWeapon>().bombPower += UnityEngine.Random.Range(1f, 2f);
    }

    [Command]
    void CmdDestroyEnemy(GameObject hitInfo)
    {
        NetworkServer.Destroy(hitInfo.gameObject);
    }

    [Command]
    void CmdDamageClone()
    {
        CloneMovement cloneHit = FindObjectOfType<CloneMovement>();
        cloneHit.currentHealth -= damage;
        Debug.Log("vita clone: " + cloneHit.currentHealth);
    }
}