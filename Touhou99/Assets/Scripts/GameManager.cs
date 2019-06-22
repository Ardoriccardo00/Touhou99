using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const string PLAYER_ID_PREFIX = "Player ";

    public static Dictionary<string, playerMovement> players = new Dictionary<string, playerMovement>();
    public static Dictionary<string, playerMovement> playersAlive = new Dictionary<string, playerMovement>();

    public delegate void OnPlayerKilledCallBack(string player, string source);
    public OnPlayerKilledCallBack onPlayerKilledCallBack;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in scene.");
        }
        else
        {
            instance = this;
        }
    }

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

    public static void AddAlivePlayer(string _netID, playerMovement _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        playersAlive.Add(_playerID, _player);
        _player.transform.name = _playerID;
        Debug.Log("Aggiunto " + _player + " tra i vivi");
    }

    public static void RemoveDeadPlayer(string _playerID)
    {
        playersAlive.Remove(_playerID);
        Debug.Log("Rimosso " + _playerID + " tra i vivi");
    }
}
