using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    public float TurnSpeed = 0.75f;
	
    // Update is called once per frame
    void FixedUpdate () {
        transform.Rotate(Vector3.up, TurnSpeed, Space.World);
    }
}
