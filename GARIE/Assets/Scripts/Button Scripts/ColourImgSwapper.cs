using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourImgSwapper : MonoBehaviour
{

    public GameObject marker;
    private bool doOnce=true;
    public string thisColString;
    public GameObject otherButton1;
    public GameObject otherButton2;
    public GameObject otherButton3;

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
        otherButton3.GetComponent<ButtonScript>().active=false;
        otherButton1.transform.GetChild(1).gameObject.SetActive(false);
        otherButton2.transform.GetChild(1).gameObject.SetActive(false);
        otherButton3.transform.GetChild(1).gameObject.SetActive(false);

        Color newColour=Color.black;

       if(thisColString=="red")
       {
            newColour=Color.red;
       }else if(thisColString=="green")
       {
            newColour=Color.green;
       }else if(thisColString=="blue")
       {
            newColour=Color.blue;
       }else if(thisColString=="black")
       {
            newColour=Color.black;
       }


        //transform.GetChild(1).gameObject.SetActive(false);  //turn off the red border
        //transform.GetComponent<ButtonScript>().active=false;    //tell the button script the button is not active.
        transform.GetComponent<ButtonScript>().StopHover();    //call stophover to reset teh material to default

        marker.GetComponent<DokoDemoPainterPen>().color=newColour;

    }
}
