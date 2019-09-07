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
    [SerializeField] float menusMoveSpeed = 1000f;

    [Header("Game Objects")]
    [SerializeField] CanvasGroup mainCanvas;
    [SerializeField] CanvasGroup findCanvas;
    [SerializeField] CanvasGroup createCanvas;
    [SerializeField] CanvasGroup optionsCanvas;
    [SerializeField] CanvasGroup girlCanvas;
    [SerializeField] CanvasGroup userStatsCanvas;
    [SerializeField] CanvasGroup loadingCanvas;

    [SerializeField] GameObject awayPoint;
    [SerializeField] GameObject centerPoint;

    [SerializeField] Rotate rotatingNine;

    //[SerializeField] GameObject menuToMoveAway;

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
        SetDefaults();

        playerNameImage = playerName.GetComponent<TextMeshProUGUI>();
        mainCanvas.alpha = 0;

        StartCoroutine(StartMenuAnimations());

        rotatingNine.GetComponent<Rotate>();
        rotatingNine.enabled = false;
    }

    void SetDefaults()
    {
        mainCanvas.gameObject.SetActive(true); //fare array di canvas
        findCanvas.gameObject.SetActive(false);
        createCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(false);
        girlCanvas.gameObject.SetActive(false);
        userStatsCanvas.gameObject.SetActive(false);
        girlCanvas.alpha = 0;
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

        for (int i = 0; i < textsContainer.Length; i++) //Move texts //-635.7, -569.7, -660.5, -633.8, -697.5
        {
            Vector3 nextTextPosition = new Vector3(textsContainer[i].transform.position.x + 770, textsContainer[i].transform.position.y, 0);

            while (textsContainer[i].transform.position != nextTextPosition)
            {
                textsContainer[i].transform.position = Vector3.MoveTowards(textsContainer[i].transform.position, nextTextPosition, Time.deltaTime * textsMoveSpeed);
                yield return null;
            }
        }

        while (playerNameImage.alpha < 0.5f) //Fade player name in
        {
            playerChangingAlpha += Time.deltaTime / 2;
            playerNameImage.color = new Color(255, 255, 255, playerChangingAlpha);
            yield return null;
        }

        ambManager.SetActive(true);

        CanvasGroup titleAlpha = titleImage.GetComponent<CanvasGroup>();
        titleAlpha.alpha = 1;
        yield return new WaitForSeconds(0.1f);
        titleAlpha.alpha = 0.5f;
        yield return new WaitForSeconds(0.1f);
        titleAlpha.alpha = 1;
        yield return new WaitForSeconds(0.1f);
        titleAlpha.alpha = 0.5f;
        yield return new WaitForSeconds(0.3f);
        titleAlpha.alpha = 1;
        rotatingNine.enabled = true;
    }

    public void InvokeMoveMenuOut(GameObject menuToMoveAway)
    {
        StartCoroutine(moveMenuOut(menuToMoveAway));
    }

    IEnumerator moveMenuOut(GameObject menu) //Move menus
    {
        while (menu.transform.position != awayPoint.transform.position) 
        {
            menu.transform.position = Vector3.MoveTowards(menu.transform.position, awayPoint.transform.position, Time.deltaTime * menusMoveSpeed);
            yield return null;
        }

        menu.gameObject.SetActive(false);        
        yield return null;   
    }

    public void InvokeMoveMenuIn(GameObject menuToMoveIn)
    {
        StartCoroutine(moveMenuIn(menuToMoveIn));
    }

    IEnumerator moveMenuIn(GameObject menuIn) //move menus in
    {
        menuIn.SetActive(true);
        yield return null;

        while (menuIn.transform.position != centerPoint.transform.position)
        {
            menuIn.transform.position = Vector3.MoveTowards(menuIn.transform.position, centerPoint.transform.position, Time.deltaTime * menusMoveSpeed);
            yield return null;
        }
    }

    public void InvokeFadeMenuOut(GameObject menu)
    {
        StartCoroutine(FadeMenuOut(menu));
    }

    public void InvokeFadeMenuIn(GameObject menu)
    {
        StartCoroutine(FadeMenuIn(menu));
    }

    IEnumerator FadeMenuOut(GameObject menu)
    {
        CanvasGroup menuCanvas = menu.GetComponent<CanvasGroup>();

        while (menuCanvas.alpha > 0) //Fade Menu in
        {
            menuCanvas.alpha -= Time.deltaTime / 0.5f;
            yield return null;
        }
    }

    IEnumerator FadeMenuIn(GameObject menu)
    {
        CanvasGroup menuCanvas = menu.GetComponent<CanvasGroup>();

        while (menuCanvas.alpha < 1)
        {
            menuCanvas.alpha += Time.deltaTime / 0.3f;
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
//public void InvokeFadeMainMenuCanvas()
//{
//    StartCoroutine(FadeMainMenuCanvas());
//}
//IEnumerator FadeMainMenuCanvas()
//{
//    while (mainCanvas.alpha > 0) //Fade Menu in
//    {
//        mainCanvas.alpha -= Time.deltaTime / 0.5f;
//        yield return null;
//    }

//    mainCanvas.gameObject.SetActive(false);
//    findCanvas.gameObject.SetActive(true); //fare fade in
//}
