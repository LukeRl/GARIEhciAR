using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCanvas : MonoBehaviour
{
	private GameObject _area;
	private GameObject _canvasGameObject;
	private GameObject[] _canvasArray;
	private GameObject _platformGameObject;

	//private float _fadePerSecond = 0.5f;
	private bool _triggered;
	
	public Transform user;

	// Start is called before the first frame update
	void Start()
    {
		_area = gameObject.transform.parent.gameObject;
		_canvasGameObject = _area.transform.Find("Canvas").gameObject;
		_canvasArray = new[]
		{
			_canvasGameObject.transform.Find("LeftCanvas").gameObject,
			_canvasGameObject.transform.Find("RightCanvas").gameObject,
			_canvasGameObject.transform.Find("FrontCanvas").gameObject,
			_canvasGameObject.transform.Find("RearCanvas").gameObject
		};
		_platformGameObject = _area.transform.Find("Platform").gameObject;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
	    if (other.tag == "Player")
	    {
		    float smallestAngle = Quaternion.Angle(Quaternion.LookRotation(user.transform.forward), Quaternion.LookRotation(_canvasArray[0].transform.forward)); ;
		    _platformGameObject.SetActive(false);
		    GameObject forwardCanvas = _canvasArray[0];

		    foreach (GameObject canvas in _canvasArray)
		    {
				float playerCanvasAngle = Quaternion.Angle(Quaternion.LookRotation(user.transform.forward), Quaternion.LookRotation(canvas.transform.forward));
				if (playerCanvasAngle < smallestAngle)
				{
					smallestAngle = playerCanvasAngle;
					forwardCanvas = canvas;
				}
		    }

			if (forwardCanvas != null)
			{
				forwardCanvas.SetActive(true);
				_canvasGameObject.SetActive(true);
		    }
	    }
    }
}
