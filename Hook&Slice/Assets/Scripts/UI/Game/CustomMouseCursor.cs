using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMouseCursor : MonoBehaviour
{

    [SerializeField]
    private Texture2D mouseCursor;

    private Vector2 hotspot = new Vector2(0, 0);

    CursorMode cursorMode = CursorMode.Auto;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(mouseCursor, hotspot, cursorMode);
    }
}
