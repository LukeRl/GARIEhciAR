using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeSwapper : MonoBehaviour
{

    public GameObject marker;
    private bool doOnce=true;
    public string thisSizeString;
    public GameObject otherButton1;
    public GameObject otherButton2;

    void Update()
    {
        if(gameObject.GetComponent<ButtonScript>().active)
        {
            
            if(doOnce)
            {
                SwapImage();
            }
            doOnce=false;
        }else
        {
            doOnce=true;
        }
    }

    public void SwapImage()
    {
        otherButton1.GetComponent<ButtonScript>().active=false;
        otherButton2.GetComponent<ButtonScript>().active=false;
    
        otherButton1.transform.GetChild(1).gameObject.SetActive(false);
        otherButton2.transform.GetChild(1).gameObject.SetActive(false);
    

        int newSize=0;

       if(thisSizeString=="big")
       {
            newSize=200;
       }else if(thisSizeString=="med")
       {
            newSize=80;
       }else if(thisSizeString=="small")
       {
            newSize=20;
       }


        //transform.GetChild(1).gameObject.SetActive(false);  //turn off the red border
        //transform.GetComponent<ButtonScript>().active=false;    //tell the button script the button is not active.
        transform.GetComponent<ButtonScript>().StopHover();    //call stophover to reset teh material to default

        marker.GetComponent<DokoDemoPainterPen>().radius=newSize;

    }
}
