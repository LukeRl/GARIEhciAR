using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaBorder : MonoBehaviour
{
	private GameObject _area;
	private GameObject _leftGlowGameObject;
	private GameObject _rightGlowGameObject;
	private GameObject _frontGlowGameObject;
	private GameObject _rearGlowGameObject;
	private GameObject _platformTriggerGameObject;
	private GameObject _adBoardSpriteGameObject;
	private GameObject _platformGameObject;
	private GameObject _canvasGameObject;
	private GameObject[] _canvasArray;
	private Vector3[] _defaultPositions;
	private Quaternion[] _defaultRotations;
	private Vector3[] _defaultScales;

	private float _fadePerSecond = 0.5f;
	private bool _triggered;
	
	private SpriteRenderer _adBoardSpriteRenderer;
	private MeshRenderer _platformRenderer;

	// Start is called before the first frame update
	void Start()
	{
		_area = gameObject.transform.parent.gameObject;
		_leftGlowGameObject = _area.transform.Find("Left").GetChild(0).gameObject;
		_rightGlowGameObject = _area.transform.Find("Right").GetChild(0).gameObject;
		_frontGlowGameObject = _area.transform.Find("Front").GetChild(0).gameObject;
		_rearGlowGameObject = _area.transform.Find("Rear").GetChild(0).gameObject;
		_platformTriggerGameObject = _area.transform.Find("PlatformTrigger").gameObject;
		_adBoardSpriteGameObject = _area.transform.Find("AdBoard").GetChild(0).gameObject;
		_platformGameObject = _area.transform.Find("Platform").gameObject;

		_canvasGameObject = _area.transform.Find("Canvas").gameObject;
		_canvasArray = new[]
		{
			_canvasGameObject.transform.Find("LeftCanvas").gameObject,
			_canvasGameObject.transform.Find("RightCanvas").gameObject,
			_canvasGameObject.transform.Find("FrontCanvas").gameObject,
			_canvasGameObject.transform.Find("RearCanvas").gameObject
		};

		_defaultPositions = new Vector3[4];
		_defaultRotations = new Quaternion[4];
		_defaultScales = new Vector3[4];

		StoreDefaults();

		_adBoardSpriteRenderer = _adBoardSpriteGameObject.GetComponent<SpriteRenderer>();
		_platformRenderer = _platformGameObject.GetComponent<MeshRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
	    if (_triggered && _adBoardSpriteRenderer.color.a >= 0f)
	    {
			Color color = _adBoardSpriteRenderer.color;
			_adBoardSpriteRenderer.color = new Color(color.r, color.g, color.b, color.a - _fadePerSecond * Time.deltaTime);
		}

	    if (!_triggered && _adBoardSpriteRenderer.color.a <= 0.7f)
		{
			Color color = _adBoardSpriteRenderer.color;
			_adBoardSpriteRenderer.color = new Color(color.r, color.g, color.b, color.a + _fadePerSecond * Time.deltaTime);
		}


	    if (!_triggered && _platformRenderer.material.color.a >= 0f)
	    {
		    Color color = _platformRenderer.material.color;
		    _platformRenderer.material.color = new Color(color.r, color.g, color.b, color.a - _fadePerSecond * Time.deltaTime);
		    if (_platformRenderer.material.color.a <= 0f)
			    _platformGameObject.SetActive(false);
	    }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_leftGlowGameObject.SetActive(true);
			_rightGlowGameObject.SetActive(true);
			_frontGlowGameObject.SetActive(true);
			_rearGlowGameObject.SetActive(true);
			_platformTriggerGameObject.SetActive(true);
			_triggered = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_leftGlowGameObject.SetActive(false);
			_rightGlowGameObject.SetActive(false);
			_frontGlowGameObject.SetActive(false);
			_rearGlowGameObject.SetActive(false);
			_adBoardSpriteGameObject.SetActive(true);
			_platformTriggerGameObject.SetActive(false);
			_triggered = false;

			foreach (GameObject canvas in _canvasArray)
			{
				if (canvas.activeSelf)
				{
					canvas.SetActive(false);
					break;
				}
			}
			
			ResetArea();

			_canvasGameObject.SetActive(false);
			CanvasMove.canvasIsMoving = false;
		}
	}

	private void StoreDefaults()
	{
		for (int i = 0; i < 4; i++)
		{
			_defaultPositions[i] = _canvasArray[i].transform.position;
			_defaultRotations[i] = _canvasArray[i].transform.rotation;
			_defaultScales[i] = _canvasArray[i].transform.localScale;
		}
	}

	private void ResetArea()
	{
		for (int i = 0; i < 4; i++)
		{
			_canvasArray[i].transform.position = _defaultPositions[i];
			_canvasArray[i].transform.rotation = _defaultRotations[i];
			_canvasArray[i].transform.localScale = _defaultScales[i];
		}
	}
}
