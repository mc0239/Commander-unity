using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour {
    
    public GameObject player;
    public float BackgroudSize;
    
    private Transform _cameraTransform;
    private Transform[] _layers;
    
    private const float Viewzone = 10;
    
    private int _leftIndex;
    private int _rightIndex;
    private float _zamikX;

	// Use this for initialization
	void Start () {
        _cameraTransform = Camera.main.transform;
        _layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            _layers[i] = transform.GetChild(i);
        }

        _leftIndex = 0;
        _rightIndex = _layers.Length - 1;

        _zamikX = transform.position.x;//gozd
	}

    private void scrollLeft() {
        _layers[_rightIndex].position = new Vector3(_layers[_leftIndex].position.x, _layers[_leftIndex].position.y, _layers[_leftIndex].position.z - BackgroudSize);
        _leftIndex = _rightIndex;
        _rightIndex--;
        if (_rightIndex < 0)
            _rightIndex = _layers.Length - 1;
    }

    private void scrollRight() {
        _layers[_leftIndex].position = new Vector3(_layers[_rightIndex].position.x, _layers[_rightIndex].position.y, _layers[_rightIndex].position.z + BackgroudSize);
        _rightIndex = _leftIndex;
        _leftIndex++;
        if (_leftIndex == _layers.Length)
            _leftIndex = 0;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    float spremembaX = 0; //zacetniX - player.transform.position.x;
        transform.position = new Vector3(_zamikX-spremembaX, transform.position.y, transform.position.z);

        if (_cameraTransform.position.z < (_layers[_leftIndex].transform.position.z + Viewzone)+BackgroudSize)
            scrollLeft();

        if (_cameraTransform.position.z > (_layers[_rightIndex].transform.position.z - Viewzone)-BackgroudSize)
            scrollRight();

    }
}
