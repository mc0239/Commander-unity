using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
	
	public float TurnSpeed = 0.9f;

	public int ScoreAmount = 100;
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate(Vector3.up, TurnSpeed, Space.World);
	}
}
