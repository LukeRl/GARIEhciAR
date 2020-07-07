using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private string mouseXinput;    // serialized input variables so that they can be named in editor instead of going nito project settings
    [SerializeField] private string mouseYinput;
    [SerializeField] private string shiftButtonName;

    [SerializeField] private Transform playerBody;  //refernce to player body's transform.

    ///////Gesture/Cursor stuff
    public CursorMode cursorMode = CursorMode.Auto;
    public Texture2D defaultHand;
    public Texture2D selectGesture;
    public Texture2D blankHand;
    public Vector2 hotSpot = Vector2.zero;
    private Vector2 hotSpotAuto;
    private bool gestureMode=false; 
    
    public bool hovering;
    public string hoverText;
    public Texture2D toolTipBackground;
 
    public int x,a,b,p,k;

    public Texture loadImg;
    public bool selecting;
    bool selected;
    //private float startTime;
    
    public Material selectedMat;
    private int progress;

    public Texture2D emptyProgressBar;
    public Texture2D fullProgressBar;
    ////////////

    private float xAxisClamp;       // a value to track how much movement there has been on the xaxis for clamping

    private bool _invertY;

    public GameObject garie;
    public Font currentFont;

    public LayerMask playerLayerMask;

    Vector3  mousePos;

    public GameObject GARIEbutton;

    private void Awake()
    {
        LockCursor(); // sets cursor mode to locked (force x=y=0)
        xAxisClamp=0;
        hotSpotAuto =  new Vector2(defaultHand.width*0.5f, defaultHand.height*0.5f);        //calculate the hotspottso that it can be the centre of the hand when the hand is in forcesoftware mode
    }
    void OnGUI()
    {
        if(hovering)
        {
            GUIContent toolTipGUIcontent = new GUIContent();        //GUIcontent for the tooltip box
            GUIStyle toolTipGUIstyle = new GUIStyle();          //GUI style object
            toolTipGUIstyle.normal.background=(Texture2D)toolTipBackground; // sets the background of the gui style to the selected texture
            toolTipGUIstyle.normal.textColor=Color.white;   // makes font white
            toolTipGUIstyle.wordWrap=true;  //makes the words wrap around the content Rect
            toolTipGUIstyle.padding=new RectOffset(5,5,5,5);    // padding so words dont hit the edges
            toolTipGUIstyle.font=currentFont;
            toolTipGUIcontent.text = hoverText; // sets the content text to the current hovertext
            GUI.Label(new Rect(Input.mousePosition.x - x,Screen.height-Input.mousePosition.y,a,b),toolTipGUIcontent,toolTipGUIstyle);       // creates a rectangle GUI label with the hovertext
        }
        if(selecting)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x - p,Screen.height-Input.mousePosition.y+k, 100, 50), emptyProgressBar);  // Draws the background part of the laoding bar
            GUI.DrawTexture(new Rect(Input.mousePosition.x - p,Screen.height-Input.mousePosition.y+k, progress, 50), fullProgressBar);  //draws the pink 'progress' part of the loading bar. starts with a width of 0 and ++'s it
            progress+=3; // incremtns progress (used for the progress REct's width)

            if(progress>=100)   // once the progress box is 'full'
            {
                selected=true;  
                selecting=false;    
                progress=0;
            }
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown(shiftButtonName))   // if shift pressed down
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; //unluck the cursor
            gestureMode = true;
            Cursor.SetCursor(defaultHand, hotSpotAuto, CursorMode.ForceSoftware);     // Switch cursor mode to ForceSoftware to allow it to be a size other than 32x32. 
			

        } else if(Input.GetButtonUp(shiftButtonName))
        {
            Cursor.visible = false;     //hide the cursor (this is done as a part of locking the cursor but for some reason if you just lock it first it flahses before hiding)
            Cursor.SetCursor(null,hotSpot,cursorMode);  //sets the cursor back to one with a normal hotspot (vector.zero). the mode and the texture are irrelevant here since it is hidden anyway in FP mode
            LockCursor();   //locks the cursor (also hides it but we do that twice because it fixes teh flash when you lift shift up... my guess is locking the cursor takes more than a frame? IDK....)
            gestureMode=false;
            hovering=false;
            selecting=false;
        } else if(gestureMode==false)
        {
            CameraRotation(); //calls the rotation function every update. how we look around.
        }else if (gestureMode==true)    //shift is held and the cursor is active
        {
	        if (!CanvasMove.canvasIsMoving && !MovePen.penIsMoving)
	        {
		        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // a ray frmo the mouse
		        RaycastHit hit;

		        if (Physics.Raycast(ray, out hit, 10000, playerLayerMask)) // if a ray sent out 10000 units from mouse location hits. mask the ray to hit everything except that player
		        {
			        //Debug.Log(hit.transform.name);
			        if (hit.transform.tag == "Button") // if the ray hit a button
			        {
				        if (selected)
				        {
                            if (hit.transform.name == "GARIEButton1"||hit.transform.name == "GARIEButton2")
                            {
                                garie.GetComponent<GarieActive>().goSleep();
                                GARIEbutton.transform.GetChild(1).gameObject.SetActive(false); //turn off the red border
						        GARIEbutton.transform.GetComponent<ButtonScript>().active = false; //tell the button script the button is not active.
						        GARIEbutton.transform.GetComponent<ButtonScript>().StopHover(); //call stophover to reset teh material to default  
                            }
					        if (hit.transform.GetComponent<ButtonScript>().active) // if the button hit is already active
					        {
						        hit.transform.GetChild(1).gameObject.SetActive(false); //turn off the red border
						        hit.transform.GetComponent<ButtonScript>().active = false; //tell the button script the button is not active.
						        hit.transform.GetComponent<ButtonScript>().StopHover(); //call stophover to reset teh material to default
						        if (hit.transform.name == "GARIEButton")
						        {
							        garie.GetComponent<GarieActive>().goSleep();
						        }
					        }
					        else
					        {
						        //button wasnt already active. select (activate) it
						        if(hit.transform.childCount > 1){
                                    hit.transform.GetChild(1).gameObject.SetActive(true); // activate the red border (first child of hit button)
                                }
						        hit.transform.GetComponent<Renderer>().material = selectedMat; // change to dark red material
						        hit.transform.GetComponent<ButtonScript>().active = true; // tell teh script it is active
						        if (hit.transform.name == "GARIEButton")
						        {
							        garie.GetComponent<GarieActive>().WakeUp();
						        }
					        }

					        selected =false; // set the selected flag back to false to allow for the selection of other buttons.
				        }

				        hovering = true;
				        hoverText = hit.transform.GetComponent<ButtonScript>().toolTipText; //set teh tooltip text to be the one from the button thatis being hovered
				        hit.transform.GetComponent<ButtonScript>().StartHover(); //call the starthover funciton on the hovered button

				        if (Input.GetMouseButtonDown(0))
				        {
					        selecting = true;
					        progress = 0; //set progress to zero to (re)start the progress bar
				        }

				        if (Input.GetMouseButtonUp(0))
				        {
					        selecting = false;
				        }
			        }
			        else if (hit.transform.tag == "TextButton")
			        {
				        hovering = true;
				        hoverText = hit.transform.GetComponent<ButtonScript>().toolTipText; //set teh tooltip text to be the one from the button thatis being hovered
				        hit.transform.GetComponent<ButtonScript>().StartHover(); //call the starthover funciton on the hovered button

				        if (selected)
				        {
					        hit.transform.GetComponent<TextChanger>().ChangeText();
					        selected = false; // set the selected flag back to false to allow for the selection of other buttons.
				        }

				        if (Input.GetMouseButtonDown(0))
				        {
					        selecting = true;
					        progress = 0; //set progress to zero to (re)start the progress bar
				        }

				        if (Input.GetMouseButtonUp(0))
				        {
					        selecting = false;
				        }
			        }


			        else //no buttons are hit by the ray. turn off hovering and selecting. NOTE: If hovering is true one frame, and you quickly
				        // move the mouse to ANOTHER button during the same frame, hovering will stay true and both will be lit up. fix? idk. cbf..
			        {
				        hovering = false;
				        selecting = false;
			        }

		        }

                if (Input.GetMouseButtonUp(0))
                {
                    selecting = false;
                    selected = false; // set the selected flag back to false to allow for the selection of other buttons.
                }

	        }
	        else if (CanvasMove.canvasIsMoving)
	        {
		        gestureMode = true;
		        CameraRotation();
	        }

	        if(Input.GetMouseButtonDown(0)) //if click down, set cursor to OK gesture
            {
                Cursor.SetCursor(selectGesture,hotSpotAuto,CursorMode.ForceSoftware);
            }else if(Input.GetMouseButtonUp(0)) // if let go of click, set back to normal hand
            {
                Cursor.SetCursor(defaultHand,hotSpotAuto,CursorMode.ForceSoftware);
            }
			// else 
            // if (MovePen.penIsMoving && Input.GetMouseButton(1))
	        // {
			// 	Cursor.SetCursor(selectGesture, hotSpotAuto, CursorMode.ForceSoftware);
			// }
			// else if (Input.GetMouseButtonUp(1))
	        // {
		    //     Cursor.SetCursor(defaultHand, hotSpotAuto, CursorMode.ForceSoftware);
			// }
        }
        
    }

    private void CameraRotation()
    {

	    if (Input.GetKeyUp(KeyCode.I))
	    {
		    _invertY = !_invertY;
	    }

		float mouseX = Input.GetAxis(mouseXinput) * mouseSensitivity * Time.deltaTime;   // getting the mouse X input (times the sensitivity and time)
        float mouseY = Input.GetAxis(mouseYinput) * mouseSensitivity * Time.deltaTime;

        mouseY *= _invertY ? -1 : 1;

        //euler->quaternion conversion makes clamping axes directly weird because you cant just give it a degree value, soo...
        xAxisClamp+=mouseY; // Track the movemnt of mouseY on the clamp
        if(xAxisClamp>90.0f)   //Limit the clamp to 90
        {
            xAxisClamp=90.0f;
            mouseY=0;   // Make mouseY =0  so that when transofrm.Rotate is called it will rotate it by 0
            ClampXrotateTo(270.0f);  //Fixes issue of slightly breaking past the clamp (idk why, i think its something to do with using update instead of fixedupdate?). 270 is stright up in euler.
        }else if(xAxisClamp<-90.0f)   //Limit the clamp to -90
        {
            xAxisClamp=-90.0f;
            mouseY=0;   // Make mouseY =0  so that when transofrm.Rotate is called it will rotate it by 0
            ClampXrotateTo(90f);  //Fixes issue of slightly breaking past the clamp (idk why, i think its something to do with using update instead of fixedupdate?). 90 is straight down in euler.
        }

        transform.Rotate(Vector3.left * mouseY);    //rotate this.transform around the x axis (which is actually upwards since it is around the x axis), by a mutiple of mouseY
        playerBody.Rotate(Vector3.up * mouseX);  //rotate the playerbody around the verticle axis (i.e looking left to right) by a multiple of the x mouse movement
    
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;       
    }

    private void ClampXrotateTo(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles; //get euler rotation
        eulerRotation.x=value;// set x rotation to given value
        transform.eulerAngles = eulerRotation;// set angle back to rotation value
    }

    IEnumerator Select()
    {
        yield return new WaitForSeconds(3);
    }
}
