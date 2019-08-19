using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserAccount_Lobby : MonoBehaviour
{
    public TextMeshProUGUI usernameText;

    void Start()
    {
        if(UserAccountManager.IsLoggedIn)
           usernameText.text = UserAccountManager.LoggedIn_Username;
    }

    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.LogOut();

    }
}
