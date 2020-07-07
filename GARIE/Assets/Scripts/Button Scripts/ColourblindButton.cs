using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourblindButton : MonoBehaviour
{
    public GameObject playerCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.GetComponent<ButtonScript>().active)
        {
           playerCam.GetComponent<Colorblind>().Type=3;
        }else
        {
            playerCam.GetComponent<Colorblind>().Type=0;
        }
    }
}
