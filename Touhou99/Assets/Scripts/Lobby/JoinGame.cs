﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

[System.Obsolete]
public class JoinGame : MonoBehaviour
{
    List<GameObject> roomList = new List<GameObject>();

    [SerializeField] private Text status;

    [SerializeField] private GameObject roomListItemPrefab;

    [SerializeField] private Transform roomListParent;

    private NetworkManager nm;

    private void Start()
    {
        nm = NetworkManager.singleton;

        if(nm.matchMaker == null)
        {
            nm.StartMatchMaker();
        }

        RefreshRoomList();
    }
    public void RefreshRoomList()
    {
        ClearRoomList();

        if(nm.matchMaker == null)
        {
            nm.StartMatchMaker();
        }

        nm.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        status.text = "";

        if (!success || matchList == null)
        {
            status.text = "Couldn't get room list.";
            return;
        }

        foreach (MatchInfoSnapshot match in matchList)
        {
            print(match.name);
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            _roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();

            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }
            roomList.Add(_roomListItemGO);
        }

        if(roomList.Count == 0)
        {
            status.text = "No rooms at the moment";
        }
    }

    void ClearRoomList()
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();

    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        if (ChooseGirl.girlChosen)
        {
            Debug.Log("Joining " + _match.name);
            StartCoroutine(WaitForJoin());
            nm.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, nm.OnMatchJoined);
        }   
    }

    public void PrepareRandomRoom()
    {
            nm.matchMaker.ListMatches(0, 20, "", true, 0, 0, JoinRandomRoom);      
    }

    private void JoinRandomRoom(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if(matchList.Count > 0)
        {
            int matchNumber = Random.Range(0, matchList.Count);
            MatchInfoSnapshot _match = matchList[matchNumber];
            if (ChooseGirl.girlChosen)
            {
                nm.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, nm.OnMatchJoined);
            }
        }
    }

    IEnumerator WaitForJoin()
    {
        ClearRoomList();

        int countdown = 5;
        while (countdown > 0)
        {
            status.text = "Joining...(" + countdown + ")";

            yield return new WaitForSeconds(1);

            countdown--;
        }

        status.text = "Failed to connect";
        yield return new WaitForSeconds(1);

        MatchInfo matchInfo = nm.matchInfo;
        if(matchInfo != null)
        {
            nm.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, nm.OnDropConnection);
            nm.StopHost();
        } 

        RefreshRoomList();
    }
}
