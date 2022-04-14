using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("드래그 시작");
    }
    public void OnDrag(PointerEventData eventData){
        Debug.Log("드래그 중");
    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("드래그 종료");
    }
}
