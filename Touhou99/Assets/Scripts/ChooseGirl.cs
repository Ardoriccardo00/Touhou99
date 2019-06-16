using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ChooseGirl : MonoBehaviour
{
    public GameObject characterSelect;

    //[System.Obsolete]
    public void PickHero(int hero)
    {
        NetworkManager.singleton.GetComponent<NetworkCustom>().chosenCharacter = hero;
        characterSelect.SetActive(false);
    }
}
