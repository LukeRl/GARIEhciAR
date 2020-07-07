using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaPlatform : MonoBehaviour
{
	private GameObject _area;
	private GameObject _platformGameObject;

	private float _fadePerSecond = 0.5f;
	private bool _triggered;

	private MeshRenderer _platformRenderer;

	// Start is called before the first frame update
	void Start()
	{
		_area = gameObject.transform.parent.gameObject;
		_platformGameObject = _area.transform.Find("Platform").gameObject;
		_platformRenderer = _platformGameObject.GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (_triggered && _platformRenderer.material.color.a <= 0.7f)
		{
			Color color = _platformRenderer.material.color;
			_platformRenderer.material.color = new Color(color.r, color.g, color.b, color.a + _fadePerSecond * Time.deltaTime);
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
			_platformGameObject.SetActive(true);
			_triggered = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			_triggered = false;
		}
	}
}
