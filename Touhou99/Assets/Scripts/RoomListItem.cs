using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;
public class RoomListItem : MonoBehaviour
{
    [SerializeField]
    private Text roomNameText;

    private MatchDesc match;

    public void Setup(MatchDesc _match)
    {
        match = _match;

        roomNameText.text = match.name + "(" + match.currentSize + "/" + match.maxSize + ")";
    }
}
