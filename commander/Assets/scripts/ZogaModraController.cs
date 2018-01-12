using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZogaModraController : MonoBehaviour {
	public GameObject player;

	public float speed;
	public float detectionRange=3.0f;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{

		Vector3 dir = player.transform.position - transform.position;
		if (dir.magnitude < detectionRange) {
			Vector3 heading;
			if (dir.y > 2) {
				heading = new Vector3 (dir.x, 1.0f, dir.z);
			} else {
				heading = dir;
			}

			rb.AddForce (heading * speed);
		}
	}
}
