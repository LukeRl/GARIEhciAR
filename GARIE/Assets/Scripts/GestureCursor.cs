using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCursor : MonoBehaviour
{

    private SpriteRenderer rend;
    public Sprite defaultCursor;
    public Sprite selectGesture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible=false;       //hide default cursor
        rend=GetComponent<SpriteRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //mouse location
        transform.position=cursorPos;   // set this objects location to mouse location 

        if(Input.GetMouseButtonDown(0))
        {
            rend.sprite=selectGesture;
        }else if(Input.GetMouseButtonUp(0))
        {
            rend.sprite=defaultCursor;
        }

    }
}
