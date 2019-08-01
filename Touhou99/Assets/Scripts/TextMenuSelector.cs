using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMenuSelector : MonoBehaviour
{
    enum Screen {Main, FindMatch, CreateMatch, Options, ChooseGirl};
    [SerializeField] Screen screen;

    private int cursor = 0;

    [Header("Texts")]
    [SerializeField] private Text[] textList;
    [SerializeField] private Text debugText;
    [SerializeField] private Text enumText;   

    [Header("Canvas")]
    [SerializeField]  private Canvas[] canvasList; //main, find, create, options, choose

    [Header("Audio")]    
    [SerializeField] AudioClip selectSound;
    [SerializeField] AudioClip confirmSound;
    [SerializeField] AudioClip cancelSound;
    AudioSource audioSource;

    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        screen = Screen.Main;
    }

    void Update()
    {
        enumText.text = Convert.ToString(screen);

        GetInputArrows();
        GetConfirmInput();
        GetCancelInput();
        ConvertText();

        switch (screen) //main, find, create, options, choose
        {
            case Screen.Main:
                DisableAllCanvas();
                canvasList[0].enabled = true;
                break;
            case Screen.FindMatch:
                DisableAllCanvas();
                canvasList[1].enabled = true;
                break;
            case Screen.CreateMatch:
                DisableAllCanvas();
                canvasList[2].enabled = true;
                break;
            case Screen.Options:
                DisableAllCanvas();
                canvasList[3].enabled = true;
                break;
            case Screen.ChooseGirl:
                DisableAllCanvas();
                canvasList[4].enabled = true;
                break;
        }
    }

    private void GetInputArrows()
    {
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            audioSource.PlayOneShot(selectSound);
            if (cursor >= textList.Length - 1) cursor = 0;
            else cursor++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            audioSource.PlayOneShot(selectSound);
            if (cursor <= 0) cursor = textList.Length - 1;
            else cursor--;
        }
    }

    private void GetCancelInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            audioSource.PlayOneShot(cancelSound);       
            switch (screen) //main, find, create, options, choose
            {
                case Screen.Main:
                    cursor = textList.Length - 1;
                    break;

                default:
                    screen = Screen.Main;
                    break;
            }
        }
    }

    private void GetConfirmInput()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            audioSource.PlayOneShot(confirmSound);
            switch (cursor)
            {
                case 0:
                    screen = Screen.FindMatch;
                    break;
                case 1:
                    screen = Screen.CreateMatch;
                    break;
                case 2:
                    screen = Screen.Options;
                    break;
                case 3:
                    LogOut();
                    break;
                case 4:
                    Invoke("QuitGame", 2f);                  
                    break;

            }
        }
    }

    void DisableAllCanvas()
    {
        for(int i = 0; i < canvasList.Length - 1; i++)
        {
            canvasList[i].enabled = false;
        }
    }

    private void ConvertText()
    {
        for (int i = 0; i < textList.Length; i++) textList[i].fontStyle = FontStyle.Normal;
        debugText.text = Convert.ToString(cursor);
        textList[cursor].fontStyle = FontStyle.BoldAndItalic;
    }

    public void LogOut()
    {
        if (UserAccountManager.IsLoggedIn)
            UserAccountManager.instance.LogOut();
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
