using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immersive : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.GetComponent<ButtonScript>().active)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }else
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
