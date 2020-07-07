using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public GameObject buttons;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            buttons.SetActive(true);
        }
     }
     
     void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            buttons.SetActive(false);
        }
     }
}
