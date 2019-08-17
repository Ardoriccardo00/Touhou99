using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class MoveMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI text;
    //[SerializeField] GameObject audioComponent;
    //[SerializeField] GameObject audio;
    void Start()
    {
        text = gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        //audio = gameObject.GetComponent<GameObject>().GetComponent<AudioSource>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Italic;
        //audio.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Normal;
    }
}
