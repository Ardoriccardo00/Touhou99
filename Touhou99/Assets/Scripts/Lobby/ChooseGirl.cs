using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class ChooseGirl : MonoBehaviour
{
    public static bool girlChosen = false;
    [SerializeField] private Text girlError;

    public void PickHero(int hero)
    {
        girlChosen = true;
        girlError.enabled = false;
        NetworkManager.singleton.GetComponent<NetworkCustom>().chosenCharacter = hero;
    }
}
