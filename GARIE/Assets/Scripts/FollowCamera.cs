using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject pCamera;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = pCamera.transform.position + new Vector3(1.703f,0.864999f,1.322f);
    }
}
