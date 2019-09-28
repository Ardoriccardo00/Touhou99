using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

[System.Obsolete]
public class NetworkCustom : NetworkManager
{
    [Header("Character Selector")]
    public int chosenCharacter = 0;
    public GameObject[] characters;

    //subclass for sending network messages
    public class NetworkMessage : MessageBase
    {
        public int chosenClass;
    }


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        GameManager.IncreaseArenaNumber();
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        int selectedClass = message.chosenClass;
        Debug.Log("server add with message " + selectedClass);

        GameObject arena;
        GameObject player;
        Transform startPos = GetStartPosition();

        GameObject playersContainer = GameObject.Find("PlayersContainer");

        if (startPos != null)
        {
            arena = Instantiate(playerPrefab, startPos.position, startPos.rotation) as GameObject;
            player = Instantiate(characters[selectedClass], startPos.position, startPos.rotation) as GameObject;
            player.transform.SetParent(playersContainer.transform);
            NetworkServer.Spawn(arena);
            NetworkServer.Spawn(player);
        }
        else
        {
            arena = Instantiate(playerPrefab, startPos.position, startPos.rotation) as GameObject;
            player = Instantiate(characters[selectedClass], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            player.transform.SetParent(playersContainer.transform);
            NetworkServer.Spawn(arena);
            NetworkServer.Spawn(player);
        }

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        NetworkMessage test = new NetworkMessage();
        test.chosenClass = chosenCharacter;

        ClientScene.AddPlayer(conn, 0, test);
    }


    public override void OnClientSceneChanged(NetworkConnection conn) //Rimuovere?
    {
        //base.OnClientSceneChanged(conn);
    }

    public void btn1()
    {
        chosenCharacter = 0;
    }

    public void btn2()
    {
        chosenCharacter = 1;
    }
}
