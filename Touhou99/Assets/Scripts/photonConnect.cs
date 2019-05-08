using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonConnect : MonoBehaviour
{
    private string gameVersion = "0.1";

    public GameObject sectionView1, sectionView2, sectionView3;

    public void connectToPhoton()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        Debug.Log("Connecting to Photton...");
    }

    private void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);

        Debug.Log("Connected to master");
    }

    private void OnJoinedLobby()
    {
        sectionView1.SetActive(false);
        sectionView2.SetActive(true);

        Debug.Log("On joined lobby!");
    }

    private void OnDisconnectedFromPhoton()
    {
        sectionView3.SetActive(true);
        Debug.Log("Disconnected from Photon!");
    }
}
