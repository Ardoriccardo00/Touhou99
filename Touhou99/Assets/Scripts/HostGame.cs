using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{

    [SerializeField]
    private uint roomSize = 6;
    private string roomName;

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

    public void createRoom()
    {
        if(roomName != "" && roomName != null)
        {
            Debug.Log("Creating room named " + roomName + " for " + roomSize + " players");
            nm.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, nm.OnMatchCreate);
        }
    }
}
