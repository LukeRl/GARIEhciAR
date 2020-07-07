using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GarieMove : MonoBehaviour
{

    public GameObject[] waypoints;
    public GameObject garriesTail;
    public GameObject garrieTextBox;
    public GameObject player;

    float rotationspeed=1.5f;
    float speed=1f;
    float accuracyWP = 0.3f;
    int currentWP=0;
    bool moving=false;
    string currentDestination;
    Vector3 garrieTextBoxInitPos = new Vector3();
    


    // Start is called before the first frame update
    public void Move(string destination)
    {
       // gameObject.A
        garrieTextBoxInitPos=garrieTextBox.transform.localPosition;
        transform.parent=null;          //decouple child and parent
        currentWP=FindClosestWP(destination);
        moving=true;
        currentDestination=destination;
        garrieTextBox.SetActive(false);
        
    }

    private int FindClosestWP(string destination)
    {
        int closestWP=0;

        if(destination=="canvas"&&currentWP==6){return 7;}

        if(waypoints.Length==0)
        {
            return -1; //flag negative value to indicate that no waypoints have been added to the array
        }
        float shortestDistance=Vector3.Distance(player.transform.position,waypoints[0].transform.position);   //value to store the distance between garie and the 'last' (starts at index of array then loops through) WP
        for(int i=1;i<waypoints.Length; i++)
        {
            float thisDistance=Vector3.Distance(player.transform.position,waypoints[i].transform.position);
            //Debug.Log("comparing distance between thisDistance:"+thisDistance+ " to waypoint:"+i+"and shortestDistance:"+shortestDistance);
            if(thisDistance<shortestDistance&& i!=currentWP)
            {
                //Debug.Log("thisDistance<lastDistance, clostetWP is now"+i);
                closestWP = i;
                shortestDistance=thisDistance;
            }
            
        }

        //Debug.Log("found closest waypoint, which is:="+closestWP);
        return closestWP;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            //Debug.Log("moving. CurrentWP="+currentWP);
            garriesTail.GetComponent<TrailRenderer>().emitting=true;
            Vector3 direction = waypoints[currentWP].transform.position-transform.position;     //find direction of current waypoint
            this.transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(direction),rotationspeed*Time.deltaTime); //rotate toward waypoint
            this.transform.Translate(0,0,Time.deltaTime*speed);     //translate forward


            if(direction.magnitude<accuracyWP)  //made it witin WP accuracy area
            {
                //Debug.Log("hit waypoint:"+currentWP);
                if(currentDestination=="art")
                {
                    //Debug.Log("looking for art");
                    if(currentWP==6)
                    {
                        moving=false;
                        transform.GetComponent<LookAtUser>().enabled=true;
                        garrieTextBox.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text="This is a piece of interactive art! If you would like ... <insert tutorial here>";
                        garrieTextBox.transform.localPosition=new Vector3(-0.407f,0.224f,0.965f);
                        garrieTextBox.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(true);
                        garrieTextBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                        garrieTextBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                        garrieTextBox.SetActive(true);

                    }else if(currentWP>6)
                    {
                        currentWP--;    //past 6, go back
                    }else 
                    {
                        currentWP++;    //not 6 or past six, must be less than. go forward
                    }
                }else if(currentDestination=="canvas")
                {
                    //Debug.Log("looking for canvas");
                    if(currentWP<3) // on 'small' side of meuseum, aim for 0
                    {
                        if(currentWP==0)
                        {
                            moving=false;       //made it
                            transform.GetComponent<LookAtUser>().enabled=true;
                            garrieTextBox.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text="This is the interactive canvas! Here, you can ... <insert tutorial here>";
                            garrieTextBox.transform.localPosition=garrieTextBoxInitPos;
                            garrieTextBox.transform.localPosition=new Vector3(-0.407f,0.224f,0.965f);
                            garrieTextBox.transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true);
                            garrieTextBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                            garrieTextBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                            garrieTextBox.SetActive(true);
                        }else
                        {
                            currentWP--;
                        }
                    }else
                    { 
                    
                        if(currentWP==7)
                        {
                            moving=false;       //made it
                            transform.GetComponent<LookAtUser>().enabled=true;
                            garrieTextBox.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text="This is the interactive canvas! Here, you can ... <insert tutorial here>";
                            garrieTextBox.transform.localPosition=garrieTextBoxInitPos;
                            garrieTextBox.transform.localPosition=new Vector3(-0.407f,0.224f,0.965f);
                            garrieTextBox.transform.GetChild(0).transform.GetChild(5).gameObject.SetActive(true);
                            garrieTextBox.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
                            garrieTextBox.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
                            garrieTextBox.SetActive(true);
                        }else if(currentWP==5)
                        {
                            currentWP=currentWP+2;    //on 5 we want to skip six becasue that is a detour to the art
                        }else
                        {
                            currentWP++;
                        }
                    }
                }
            }
        }
        else
        {
            garriesTail.GetComponent<TrailRenderer>().emitting=false;
        }
        
    }
}
