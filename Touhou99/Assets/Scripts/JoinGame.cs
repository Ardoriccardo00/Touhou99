using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour
{
    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private Text status;

    [SerializeField]
    private GameObject roomListItemPrefab;

    [SerializeField]
    private Transform roomListParent;

    [System.Obsolete]
    private NetworkManager nm;

    [System.Obsolete]
    private void Start()
    {
        nm = NetworkManager.singleton;

        if(nm.matchMaker == null)
        {
            nm.StartMatchMaker();
        }

        RefreshRoomList();
    }

    [System.Obsolete] //test
    public void RefreshRoomList()
    {
        nm.matchMaker.ListMatches(0, 20, "", false, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    [System.Obsolete]
    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        status.text = "";

        //if(matchList = null)
        //{
        //    status.text = "Couldn't get room list";
        //    return;
        //}

        ClearRoomList();

        foreach (MatchInfoSnapshot match in matches)
        {
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            _roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();

            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }
            //bla bla bla tutorial
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

    }//a

    [System.Obsolete] //test
    public void JoinRoom(MatchInfoSnapshot _match)
    {
        Debug.Log("joining " + _match.name);
        nm.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, nm.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining...";
    }
}
