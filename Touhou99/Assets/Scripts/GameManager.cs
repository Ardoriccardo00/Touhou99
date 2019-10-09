using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

[System.Obsolete]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private const string PLAYER_ID_PREFIX = "Player ";

    [SerializeField] public static Dictionary<string, Player> players = new Dictionary<string, Player>();
    [SerializeField] public static Dictionary<string, Player> playersAlive = new Dictionary<string, Player>();

    public static int numberOfArenas = 0;

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

    public static void IncreaseArenaNumber()
    {
        numberOfArenas++;
        print("Number of arenas: " + numberOfArenas);
    }

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnRegisterPlayer(string _playerID)
    {
        players.Remove(_playerID);
        print("Unregistered " + _playerID);
    }

    public static Player GetPlayer(string _pplayerID)
    {
        return players[_pplayerID];
    }

    public static Player[] GetAllPlayers()
    {
        return players.Values.ToArray();
    }

    public static void AddAlivePlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        playersAlive.Add(_playerID, _player);
        _player.transform.name = _playerID;
        Debug.Log("Aggiunto " + _player + " tra i vivi");
    }

    public static void RemoveDeadPlayer(string _ppplayerID)
    {
        playersAlive.Remove(_ppplayerID);
        Debug.Log("Rimosso " + _ppplayerID + " tra i vivi");
    }
}
