using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; set;}
    public delegate void OnMouseLeftClickEvent();
    public static event OnMouseLeftClickEvent onMouseLeftClickEvent = delegate{};

    // public Vector3 GetWorldMousePosition()
    // {
    //     Vector3 mousePos = Input.mousePosition;
        
    //     Vector3 worldMousePosition = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
    //     worldMousePosition.z = 0;
    //     return worldMousePosition;
    // }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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
