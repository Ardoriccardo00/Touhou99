using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private const string PLAYER_ID_PREFIX = "Player ";

    public static Dictionary<string, playerMovement> players = new Dictionary<string, playerMovement>();

    [System.Obsolete]
    public static void RegisterPlayer(string _netID, playerMovement _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    [System.Obsolete]
    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
    }

    [System.Obsolete]
    public static playerMovement GetPlayer(string _playerID)
    {
        return players[_playerID];
    }

    public static playerMovement[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    //private void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200,200,200,500));

    //    GUILayout.BeginVertical();

    //    foreach(string _playerID in players.Keys)
    //    {
    //        GUILayout.Label(_playerID + "  -  " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();

    //    GUILayout.EndArea();
    //}
}
