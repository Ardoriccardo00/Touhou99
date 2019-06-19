using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
public class RoomListItem : MonoBehaviour
{
    [System.Obsolete]
    public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);

    [System.Obsolete]
    private JoinRoomDelegate joinRoomCallback;

    [SerializeField]
    private Text roomNameText;

    [System.Obsolete]
    public MatchInfoSnapshot match;

    [System.Obsolete]
    public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
    {
        match = _match;
        joinRoomCallback = _joinRoomCallback;

        roomNameText.text = match.name + "(" + match.currentSize + "/" + match.maxSize + ")";
    }

    [System.Obsolete] //test
    public void JoinRoom()
    {
        joinRoomCallback.Invoke(match);
    }
}
