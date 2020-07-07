using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.eulerAngles = new Vector3(Random.Range(21, 159), -30f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right *speed*Time.deltaTime);
        if(transform.rotation.eulerAngles.x<20||transform.rotation.eulerAngles.x>160)
        {
            speed=-speed;
        }
    }
}
