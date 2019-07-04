using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChooseGirl : MonoBehaviour
{
    public static bool girlChosen = false;

    [SerializeField]
    private Text girlError;

    [System.Obsolete]
    public void PickHero(int hero)
    {
        girlChosen = true;
        girlError.enabled = false;
        NetworkManager.singleton.GetComponent<NetworkCustom>().chosenCharacter = hero;
    }
}
