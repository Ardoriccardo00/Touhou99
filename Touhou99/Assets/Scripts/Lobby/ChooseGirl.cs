using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Obsolete]
public class ChooseGirl : MonoBehaviour
{
    public static bool girlChosen = false;
    [SerializeField] private Text girlErrorGirlCanvas;
    [SerializeField] private Text girlErrorFindCanvas;

    public void PickHero(int hero)
    {
        girlChosen = true;
        girlErrorGirlCanvas.enabled = false;
        girlErrorFindCanvas.enabled = false;
        NetworkManager.singleton.GetComponent<NetworkCustom>().chosenCharacter = hero;
    }
}
