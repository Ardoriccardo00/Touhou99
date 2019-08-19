using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.LogOut();
    }

    public void InvokeQuitGame()
    {
        Invoke("QuitGame", 1f);
    }
    void QuitGame()
    {
        Application.Quit();
    }
}
