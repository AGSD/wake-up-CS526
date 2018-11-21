using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class joybutton : MonoBehaviour,IPointerUpHandler,IPointerDownHandler {
    [HideInInspector]
    public bool pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
