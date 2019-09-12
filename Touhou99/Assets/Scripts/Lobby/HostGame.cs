﻿using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HostGame : MonoBehaviour
{


    [SerializeField]  private uint roomSize;
    private string roomName;

    public static string hosterName;

    public Text userText;

    private NetworkManager nm;

    void Start()
    {
        nm = NetworkManager.singleton;

        if(nm.matchMaker == null)
        {
            nm.StartMatchMaker();
        }
    }

    public void setRoomName(string _name)
    {
        roomName = _name;
    }

    public void setHostName(string _host)
    {
        hosterName = _host;
        userText.text = _host;
    }

    public void CreateRoom()
    {
        Invoke("CreatingRoom", 1.5f);
    }

    private void CreatingRoom()
    {
        if (roomName != "" && roomName != null && ChooseGirl.girlChosen == true)
        {
            print("Creating room named " + roomName + " for " + roomSize + " players, created by: " + hosterName);
            nm.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, nm.OnMatchCreate);
        }
    }

    IEnumerator WaitForCreate()
    {
        yield return new WaitForSeconds(2);
        //print("Creating room named " + roomName + " for " + roomSize + " players, created by: " + hosterName);
        //nm.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, nm.OnMatchCreate);
    }
}
