using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMove : MonoBehaviour
{
	public GameObject Canvas;
	private GameObject _player;
	private GameObject _canvasGroupGameObject;

	private BoxCollider _hoveredBoxCollider;
	private SpriteRenderer _activeSprite;

	private float _fadePerSecond = 0.5f;
	private bool _entered;

	[SerializeField]
	public static bool canvasIsMoving { get; set; }

    // Start is called before the first frame update
    void Start()
    {
	    Physics.queriesHitTriggers = true;
	    _player = GameObject.Find("Player");
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
	    canvasIsMoving = true;
		Cursor.lockState = CursorLockMode.Locked;
		_canvasGroupGameObject = Canvas.transform.parent.gameObject;
		Canvas.transform.SetParent(_player.transform);
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

    void OnMouseUp()
    {
		Canvas.transform.SetParent(_canvasGroupGameObject.transform);
		Cursor.lockState = CursorLockMode.None;
	    canvasIsMoving = false;
	}
}
