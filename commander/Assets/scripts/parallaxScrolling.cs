using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxScrolling : MonoBehaviour {
    public float backgroudSize;
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewzone = 10;
    private int leftIndex;
    private int rightIndex;

	// Use this for initialization
	void Start () {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
		
	}

    private void scrollLeft() {
        int lastRight = rightIndex;
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x, layers[leftIndex].position.y, layers[leftIndex].position.z - backgroudSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void scrollRight() {
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x, layers[rightIndex].position.y, layers[rightIndex].position.z + backgroudSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (cameraTransform.position.z < (layers[leftIndex].transform.position.z + viewzone)+backgroudSize)
            scrollLeft();

        if (cameraTransform.position.z > (layers[rightIndex].transform.position.z - viewzone)-backgroudSize)
            scrollRight();

    }
}
