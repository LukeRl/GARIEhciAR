using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageImgChanger : MonoBehaviour
{

    public SpriteRenderer mainSprite;
    public GameObject mainObj;
    public GameObject otherButton;
    public GameObject eventManager;
    private bool doOnce=true;
    public string thisLang;

    void Update()
    {
        transform.GetComponent<ButtonScript>().toolTipText="Change the language to: "+thisLang;
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
        Sprite oldSprite = mainSprite.sprite;
        string oldString = mainObj.GetComponent<CurrentLanguage>().currLang;

       

        mainObj.GetComponent<CurrentLanguage>().currLang=thisLang;
        thisLang = oldString;

        mainSprite.sprite = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite=oldSprite;

        transform.GetChild(1).gameObject.SetActive(false);  //turn off the red border
        transform.GetComponent<ButtonScript>().active=false;    //tell the button script the button is not active.
        transform.GetComponent<ButtonScript>().StopHover();    //call stophover to reset teh material to default

        eventManager.GetComponent<LanguageMonitor>().UpdateLanguage();
        mainObj.GetComponent<ButtonScript>().active=false;
        mainObj.transform.GetChild(1).gameObject.SetActive(false);
        

    }
}
