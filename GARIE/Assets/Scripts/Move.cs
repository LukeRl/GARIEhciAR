using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController charController;
    [SerializeField] private float moveSpeed; 
    [SerializeField] private string vertInputName; //vertical input - serialized to enable user setting without editing project settings. 
    [SerializeField] private string horizontalInputName;    //horizopntal input
    
    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float verticalInput = Input.GetAxis(vertInputName)*moveSpeed;
        float horizontalInput = Input.GetAxis(horizontalInputName)*moveSpeed;

        Vector3 forwardMovement= transform.forward*verticalInput;
        Vector3 sideMovement= transform.right*horizontalInput;

        charController.SimpleMove(forwardMovement+sideMovement); 
    }
}
