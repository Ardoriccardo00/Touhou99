using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI deathCount;
    public TextMeshProUGUI killCount;

    // Start is called before the first frame update
    void Start()
    {
        if(UserAccountManager.IsLoggedIn)
        UserAccountManager.instance.GetData(OnReceivedData);
    }

    void OnReceivedData(string data)
    {
        if (killCount == null || deathCount == null)
            return;

        killCount.text = DataTranslator.DataToKills(data).ToString() + " Kills";
        deathCount.text = DataTranslator.DataToDeaths(data).ToString() + " Deaths";
    }
}
