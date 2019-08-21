using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField] GameObject titleImage;
    [SerializeField] GameObject titlePoint;

    [SerializeField] GameObject girlImage;
    [SerializeField] GameObject girlPoint;

    [SerializeField] GameObject playerName;
    [SerializeField] GameObject playerNamePoint;

    TextMeshProUGUI playerNameImage;
    private void Start()
    {     
        playerNameImage = playerName.GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        StartCoroutine(StartMenuAnimations());
    }

    IEnumerator StartMenuAnimations()
    {
        float changingAlpha = 0;

        while (canvasGroup.alpha < 1) //Fade Menu in
        {
            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }

        while (titleImage.transform.position != titlePoint.transform.position) //Move title
        {
            titleImage.transform.position = Vector3.MoveTowards(titleImage.transform.position, titlePoint.transform.position, Time.deltaTime * 1000f);
            yield return null;
        }

        while (girlImage.transform.position != girlPoint.transform.position) //Move girl image
        {
            girlImage.transform.position = Vector3.MoveTowards(girlImage.transform.position, girlPoint.transform.position, Time.deltaTime * 1000f);
            yield return null;
        }

        playerNameImage.alpha = 0;

        while (playerName.transform.position != playerNamePoint.transform.position) //Move player name in
        {
            playerName.transform.position = Vector3.MoveTowards(playerName.transform.position, playerNamePoint.transform.position, Time.deltaTime * 1000f);
            yield return null;
        }

        while (playerNameImage.alpha < 1) //Fade player name in
        {
            changingAlpha += Time.deltaTime / 2;
            print("fade name in");
            playerNameImage.color = new Color(255, 255, 255, changingAlpha);
            //playerNameImage.alpha += Time.deltaTime / 2;
            yield return null;
        }            
    }

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

/*IEnumerator StartMenuAnimations()
    {       
        while (canvasGroup.alpha < 1)
        {           
            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }

        while(titleImage.transform.position != titlePoint.transform.position)
        {
            titleImage.transform.position = Vector3.MoveTowards(titleImage.transform.position, titlePoint.transform.position, Time.deltaTime * 1000f);
            yield return null;
        }

        while (girlImage.transform.position != girlPoint.transform.position)
        {
            girlImage.transform.position = Vector3.MoveTowards(girlImage.transform.position, girlPoint.transform.position, Time.deltaTime * 1000f);
            yield return null;
        }
    }*/
