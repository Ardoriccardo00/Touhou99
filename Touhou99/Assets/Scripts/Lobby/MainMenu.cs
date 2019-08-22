using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    
    [Header("Values")]
    [SerializeField] float titleMoveSpeed = 1000f;
    [SerializeField] float girlImageMoveSpeed = 1000f;
    [SerializeField] float textsMoveSpeed = 1000f;

    [Header("Game Objects")]
    [SerializeField] CanvasGroup mainCanvas;
    [SerializeField] CanvasGroup findCanvas;
    [SerializeField] CanvasGroup createCanvas;
    [SerializeField] CanvasGroup optionsCanvas;

    [Header("Main Menu")]
    [SerializeField] GameObject titleImage;
    [SerializeField] GameObject titlePoint;

    [SerializeField] GameObject girlImage;
    [SerializeField] GameObject girlPoint;

    [SerializeField] GameObject playerName;
    [SerializeField] GameObject playerNamePoint;

    [SerializeField] GameObject[] textsContainer;
    [SerializeField] GameObject textsPoint;

    [SerializeField] GameObject ambManager;

    TextMeshProUGUI playerNameImage;
    private void Start()
    {
        playerNameImage = playerName.GetComponent<TextMeshProUGUI>();

        //canvasGroup = GetComponent<CanvasGroup>();
        mainCanvas.alpha = 0;

        StartCoroutine(StartMenuAnimations());
    }

    IEnumerator StartMenuAnimations()
    {
        float playerChangingAlpha = 0;

        while (mainCanvas.alpha < 1) //Fade Menu in
        {
            mainCanvas.alpha += Time.deltaTime / 0.5f;
            yield return null;
        }

        while (titleImage.transform.position != titlePoint.transform.position) //Move title
        {
            titleImage.transform.position = Vector3.MoveTowards(titleImage.transform.position, titlePoint.transform.position, Time.deltaTime * titleMoveSpeed);
            yield return null;
        }

        while (girlImage.transform.position != girlPoint.transform.position) //Move girl image
        {
            girlImage.transform.position = Vector3.MoveTowards(girlImage.transform.position, girlPoint.transform.position, Time.deltaTime * girlImageMoveSpeed);
            yield return null;
        }

        playerNameImage.alpha = 0;


        while (playerName.transform.position != playerNamePoint.transform.position) //Move player name in
        {
            playerName.transform.position = Vector3.MoveTowards(playerName.transform.position, playerNamePoint.transform.position, Time.deltaTime * 1000f);
            yield return null;
        }

        for(int i = 0; i < textsContainer.Length; i++) //Move texts //-635.7, -569.7, -660.5, -633.8, -697.5
        {
            Vector3 nextTextPosition = new Vector3(textsContainer[i].transform.position.x + 1000, textsContainer[i].transform.position.y, 0);

            while (textsContainer[i].transform.position != nextTextPosition) 
            {        
                textsContainer[i].transform.position = Vector3.MoveTowards(textsContainer[i].transform.position, nextTextPosition, Time.deltaTime * textsMoveSpeed);
                yield return null;
            }
        }

        while (playerNameImage.alpha < 1) //Fade player name in
        {
            playerChangingAlpha += Time.deltaTime / 2;
            playerNameImage.color = new Color(255, 255, 255, playerChangingAlpha);
            yield return null;
        }

        ambManager.SetActive(true);
    }

    public void InvokeStartFindMatchAnimation()
    {
        StartCoroutine(StartFindMatchAnimation());
    }

    public IEnumerator StartFindMatchAnimation()
    {
        while (findCanvas.transform.position != new Vector3(0,0,0)) //Move title
        {
            findCanvas.transform.position = Vector3.MoveTowards(findCanvas.transform.position, new Vector3(0,0,0), Time.deltaTime * titleMoveSpeed);
            //yield return null;
        }
        yield return null;
    }

    public void InvokeMoveMenuOut()
    {
        StartCoroutine(moveMenuOut());
    }

    IEnumerator moveMenuOut()
    {
        float distance = 100f;
        while(distance > 0)
        {
            mainCanvas.transform.position = new Vector2(-1, 0);
            distance -= Time.deltaTime / 2;
        }

        yield return null;
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
