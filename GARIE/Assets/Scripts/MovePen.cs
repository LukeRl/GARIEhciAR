using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePen : MonoBehaviour
{
	DokoDemoPainterPen penScript;
	private Vector2 _prevMouse;

	public GameObject PenGameObject;
	public GameObject EraserGameObject;
	private GameObject _otherTool;

	[SerializeField]
	public static bool penIsMoving;

	// Start is called before the first frame update
	void Start()
    {
        penScript = gameObject.GetComponent<DokoDemoPainterPen>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (_otherTool != null)
	    {
		    if (Input.GetMouseButtonDown(1))
		    {
				Cursor.visible = false;
			    _prevMouse = Input.mousePosition;
			    _otherTool.SetActive(false);
		    }
		    else if (Input.GetMouseButton(1))
		    {
				Cursor.visible = false;
			    penScript.penDown = false;
			    _otherTool.SetActive(false);
			    SetLocalTransformPosition();
		    }
		    else if (Input.GetMouseButtonUp(1))
		    {
				Cursor.visible = true;
			    _otherTool.SetActive(true);
			    _otherTool = null;
		    }
	    }
    }

    void OnMouseDown()
    {
	    _prevMouse = Input.mousePosition;
	    _otherTool.SetActive(false);
	}

    void OnMouseOver()
	{
		penIsMoving = true;
		_otherTool = gameObject == PenGameObject ? EraserGameObject : PenGameObject;
	}

    void OnMouseExit()
	{
		penIsMoving = false;
		if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
		{
			_otherTool = null;
		}
	}

    void OnMouseDrag()
    {
	    Cursor.visible = false;
	    _otherTool.SetActive(false);
	    penScript.penDown = true;
	    SetLocalTransformPosition();
    }

    void OnMouseUp()
    {
        Cursor.visible = true;
        penScript.penDown=false;
        _otherTool.SetActive(true);
        _otherTool = null;
    }

    private void SetLocalTransformPosition()
    {
	    Vector2 currentMouse = Input.mousePosition;

		float xOffset = Mathf.Abs(_prevMouse.x - currentMouse.x) / 1000;
	    float yOffset = Mathf.Abs(_prevMouse.y - currentMouse.y) / 1000;

	    float xPos = Input.mousePosition.x < _prevMouse.x ? transform.localPosition.x - xOffset : transform.localPosition.x + xOffset;
	    float yPos = Input.mousePosition.y < _prevMouse.y ? transform.localPosition.y - yOffset : transform.localPosition.y + yOffset;

		transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);

		_prevMouse = currentMouse;
	}
}


