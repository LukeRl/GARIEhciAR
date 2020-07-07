using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarieActive : MonoBehaviour
{

    public GameObject garieWrapper;
    public GameObject playerCam;
    public GameObject garieText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WakeUp()
    {
        transform.parent.transform.parent = playerCam.transform;
        garieWrapper.transform.GetComponent<LookAtUser>().enabled=false;
        garieWrapper.transform.localPosition = new Vector3(0.478f,0.364f,1.027f);        //reset the local (relative to player) position and rotation of garie and his wrapper
        garieWrapper.transform.localEulerAngles = new Vector3(4.627f,236.752f,0);
        this.transform.localPosition = new Vector3(0.1177809f,0.1701112f,-1.328045f);
        this.transform.localEulerAngles = new Vector3(2.482f, 447.228f, 20.43f);
        this.gameObject.SetActive(true);
        transform.GetChild(6).gameObject.SetActive(true);       //turn on the text box

    }

    public void goSleep()
    {
        if(garieText.GetComponent<GarieText>().hasReadAllText){
            transform.GetChild(6).transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false); //set the text box's text's button to inactive
            transform.GetChild(6).transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(false); //set the text box's text's button to inactive
            transform.GetChild(6).transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(false); //set the text box's text's button to inactive
            transform.GetChild(6).transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true); //set the text box's text's button to inactive
            transform.GetChild(6).transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(true); //set the text box's text's button to inactive
            transform.GetChild(6).transform.GetChild(0).GetComponent<TextMeshPro>().text="Hello again! Where to?";
        }
       
        this.gameObject.SetActive(false);
    }
}
