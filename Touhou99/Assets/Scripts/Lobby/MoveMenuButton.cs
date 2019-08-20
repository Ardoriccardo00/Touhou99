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

    Color transparent = new Color(255, 255, 255, 0.5f);
    Color opaque = new Color(255, 255, 255, 255);

    void Start()
    {
        text = gameObject.GetComponentInChildren(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = opaque;       
        audioComponent.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = transparent;
    }
}
