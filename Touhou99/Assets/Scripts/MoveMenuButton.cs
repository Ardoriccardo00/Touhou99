using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveMenuButton : MonoBehaviour, IPointerEnterHandler
{
   public Vector3 startingPosition;

    void start()
    {
        //startingPosition = this.transform.position;
    }

    void update()
    {
        this.transform.position = startingPosition;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        print("puntatore");
        transform.Translate(new Vector3(10, 0, 0));
    }

    public void OnPoinerExit(PointerEventData eventData)
    {
        print("niente puntatore");
    }

    //public void OnSelect(BaseEventData eventData)
    //{
    //    print("puntatore");
    //}
}
