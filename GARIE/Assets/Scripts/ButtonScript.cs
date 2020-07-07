using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public Material defaultMat;
    public Material hoverMat;
    public string toolTipText;
    private bool matChanged;
    public GameObject playerCamera;
    Renderer rend;
    public bool active; 

    void Start()
    {
        rend = GetComponent<Renderer>();    //set the renderer to the objects renderer
        matChanged=false;
    }

    void Update()
    {
        if(!playerCamera.GetComponent<Look>().hovering)   //if the player isn't hovering anything
        {
            if(!active) // if the button isnt active
            {
                rend.material = defaultMat;
                matChanged=false;   //reset the flag for the changed material (used to only change it once)
            }
        }
        
    }

    public void StartHover()
    {   
        if(!matChanged)
        {
            rend.material = hoverMat;
            matChanged=true;
        }
    }
    public void StopHover()
    {   
        rend.material = defaultMat;
        matChanged=false;

    }
}
