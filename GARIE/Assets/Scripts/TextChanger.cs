using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChanger : MonoBehaviour
{
    public GameObject text;
    public GameObject garrieWrapper;

    public enum buttonMode{ Prev, Next, Canvas, View }

    public buttonMode mode;

    public void ChangeText()
    {
        if(mode==buttonMode.Prev)
        {
            text.transform.GetComponent<GarieText>().PrevText();
        } else if(mode==buttonMode.Next)
        {
            text.transform.GetComponent<GarieText>().NextText();
        }else if(mode==buttonMode.Canvas)
        {
            garrieWrapper.transform.GetComponent<GarieMove>().Move("canvas");
        }else if(mode==buttonMode.View)
        {
            garrieWrapper.transform.GetComponent<GarieMove>().Move("art");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
