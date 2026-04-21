using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class InputManager : Manager<InputManager>
{
    public delegate void OnMouseLeftClickEvent();
    public static event OnMouseLeftClickEvent onMouseLeftClickEvent = delegate{};

    public static Vector3 GetWorldMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        worldMousePosition.z = 0;
        return worldMousePosition;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
        if (Input.GetMouseButtonDown(0))
        {
            onMouseLeftClickEvent.Invoke();
        }
    }
}
