using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class MoveMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI text;
    [SerializeField] AudioSource audioComponent;
    //[SerializeField] GameObject audio;
    void Start()
    {
        text = gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Italic;
        audioComponent.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = FontStyles.Normal;
    }
}
