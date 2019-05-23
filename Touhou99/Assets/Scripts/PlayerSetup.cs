using UnityEngine;
using UnityEngine.Networking;

[System.Obsolete]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]

    Behaviour[] componentsToDisable;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for(int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
