using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtUser : MonoBehaviour
{
	public Transform user;
	public float rotationSpeed = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        //transform.LookAt(user);
    }

    // Update is called once per frame
    void Update()
    {
		//transform.LookAt(user);

        Quaternion rotation = Quaternion.LookRotation(user.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
	}
}
