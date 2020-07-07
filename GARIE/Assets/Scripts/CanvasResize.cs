using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasResize : MonoBehaviour
{
	public BoxCollider LeftCollider;
	public BoxCollider RightCollider;
	public BoxCollider TopCollider;
	public BoxCollider BottomCollider;

	public GameObject Canvas;

	private Vector2 _previousPoint;

	private BoxCollider _hoveredBoxCollider;
	private SpriteRenderer _activeSprite;

	private float _fadePerSecond = 0.5f;
	private bool _entered;

	// Start is called before the first frame update
	void Start()
	{
		Physics.queriesHitTriggers = true;
	}

    // Update is called once per frame
    void Update()
	{
		if (_activeSprite != null)
		{
			if (_entered && _activeSprite.color.a <= 0.8f)
			{
				Color color = _activeSprite.color;
				_activeSprite.color = new Color(color.r, color.g, color.b, color.a + _fadePerSecond * Time.deltaTime);
			}

			if (!_entered && _activeSprite.color.a >= 0.25f)
			{
				Color color = _activeSprite.color;
				_activeSprite.color = new Color(color.r, color.g, color.b, color.a - _fadePerSecond * Time.deltaTime);
			}
		}
	}

    void OnMouseDown()
    {
	    _previousPoint = Input.mousePosition;
    }

    void OnMouseDrag()
    {
	    BoxCollider clickedBoxCollider = GetComponent<BoxCollider>();

	    Vector2 currentPoint = Input.mousePosition;

	    if (clickedBoxCollider == LeftCollider || clickedBoxCollider == RightCollider)
	    {
		    float sizeChangeX = 0f;
		    
			if (currentPoint.x > _previousPoint.x)
			{
				sizeChangeX = clickedBoxCollider == LeftCollider
					? Mathf.Abs(currentPoint.x - _previousPoint.x)
					: -Mathf.Abs(currentPoint.x - _previousPoint.x);
			}
		    else if (currentPoint.x < _previousPoint.x)
			{
				sizeChangeX = clickedBoxCollider == LeftCollider
					? -Mathf.Abs(currentPoint.x - _previousPoint.x)
					: Mathf.Abs(currentPoint.x - _previousPoint.x);
			}
			Vector3 newSize = new Vector3(Canvas.transform.localScale.x + sizeChangeX / 1000, Canvas.transform.localScale.y, Canvas.transform.localScale.z);
			Canvas.transform.localScale = newSize;
		}
		else if (clickedBoxCollider == TopCollider || clickedBoxCollider == BottomCollider)
	    {
		    float sizeChangeY = 0f;

		    if (currentPoint.y > _previousPoint.y)
		    {
			    sizeChangeY = clickedBoxCollider == TopCollider
				    ? Mathf.Abs(currentPoint.y - _previousPoint.y)
				    : -Mathf.Abs(currentPoint.y - _previousPoint.y);
		    }
		    else if (currentPoint.x < _previousPoint.x)
		    {
			    sizeChangeY = clickedBoxCollider == TopCollider
					? -Mathf.Abs(currentPoint.y - _previousPoint.y)
				    : Mathf.Abs(currentPoint.y - _previousPoint.y);
		    }
		    Vector3 newSize = new Vector3(Canvas.transform.localScale.x, Canvas.transform.localScale.y + sizeChangeY / 1000, Canvas.transform.localScale.z);
		    Canvas.transform.localScale = newSize;
		}

	    _previousPoint = currentPoint;
    }

    void OnMouseEnter()
	{
		_entered = true;
		_hoveredBoxCollider = GetComponent<BoxCollider>();
		_activeSprite = _hoveredBoxCollider.gameObject.GetComponentInChildren<SpriteRenderer>();
	}

    void OnMouseExit()
	{
		_entered = false;
	}
}
