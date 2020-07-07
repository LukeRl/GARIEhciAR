using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarieText : MonoBehaviour
{
    string[] textOptions = new string[] {"Howdy, I'm GARIE and <insert tutorial here>. If you'd like me to leave you alone to explore on your own feel free to press the GARIE button in the top right once more", "<more tutorial stuff>", "Okay now that you know the basics, would you like me to lead you to view some art or have a go at the virtual canvas?"};
    public int currentTextOption = 0;
    public bool hasReadAllText=false;

    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    public void NextText()
    {   
        //Debug.Log("NEXT WAS HIT. CurrentTextOption="+currentTextOption+" and textOptions.Length ="+textOptions.Length);
        if(currentTextOption<textOptions.Length-1)
        {
            //Debug.Log("currentTextOption<textOptions.Length");
            currentTextOption++;
            updateText();
        }
          
    }
    public void PrevText()
    {
        if(currentTextOption>0)
        {
            currentTextOption--;
            updateText();
        }
        
    }

    void updateText()
    {
        gameObject.GetComponent<TextMeshPro>().text=textOptions[currentTextOption];   
        if(currentTextOption==textOptions.Length-1)   // if we are on the last text option (asking where player wants to go)
        {
            hasReadAllText=true;
            transform.GetChild(1).gameObject.SetActive(false);  //deactive the next button
            transform.GetChild(2).gameObject.SetActive(true);   //active the canvas button
            transform.GetChild(3).gameObject.SetActive(true);   //activate the view art button
        }else   //if on any other text option
        {
            transform.GetChild(1).gameObject.SetActive(true);  //active the next button
            transform.GetChild(2).gameObject.SetActive(false);   //deactive the canvas button
            transform.GetChild(3).gameObject.SetActive(false);   //deactivate the view art button
        }
    }
}
