using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class Killfeed : MonoBehaviour
{
    [SerializeField] GameObject killFeedItemPrefab;

    void Start()
    {
        GameManager.instance.onPlayerKilledCallBack += OnKill;
    }

    public void OnKill(string player, string source)
    {
       GameObject go = Instantiate(killFeedItemPrefab, this.transform);
        go.GetComponent<KillFeedItem>().Setup(player, source);
        Destroy(go, 4f);
    }

}
