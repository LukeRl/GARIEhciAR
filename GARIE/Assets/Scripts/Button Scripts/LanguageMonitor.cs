using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageMonitor : MonoBehaviour
{
    public GameObject mainLngObj;

    public TMP_FontAsset FontAssetEnglish;
    public TMP_FontAsset FontAssetArabic;
    public TMP_FontAsset FontAssetChinese;
    private TMP_FontAsset fontToBe;

    public Font FontEnglish;
    public Font FontArabic;
    public Font FontChinese;
    private Font fontNormalToBe;

    public TMP_Text descriptionText;
    public TMP_Text garyText;

    public Look lookscript;
  //  public TMP_FontAsset FontAssetB;
 
   // public Material FontMaterialA;
    //public Material FontMaterialB;

    //private TMP_Text m_TextComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLanguage()
    {

        //TMP_FontAsset fontToBe;
        string thislang=mainLngObj.GetComponent<CurrentLanguage>().currLang;
        TMP_Text[] foundObjects = FindObjectsOfType<TMP_Text>();

        if(thislang=="english")
        {
            fontToBe=FontAssetEnglish;
            fontNormalToBe=FontEnglish;
        }else if(thislang=="arabic")
        {
            fontToBe=FontAssetArabic;
            fontNormalToBe=FontArabic;
        }else if(thislang=="chinese")
        {
            fontToBe=FontAssetChinese;
            fontNormalToBe=FontChinese;
        }else{Debug.Log("ahaaaaa");}

        descriptionText.font=fontToBe;              // for painting decription font
        garyText.font=fontToBe;              // for painting decription font
        lookscript.currentFont=fontNormalToBe;      // for tooptip font

        foreach(TMP_Text text in foundObjects)
        {
               text.font = fontToBe;                // for all other found textmesh fonts
        }

    }
}