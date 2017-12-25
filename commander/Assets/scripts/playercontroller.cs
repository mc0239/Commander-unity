using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {
    public float moveSpeed = 7f;
	public float jumpForce = 18f;
	public float gravity = 6f;

	private Vector3 movement = Vector3.zero;

	private CharacterController cc;

	// Initialize
	void Start () {
        cc = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {

		float moveDirection = Input.GetAxis ("Horizontal");
		movement.z = moveDirection * moveSpeed;


		movement.y -= gravity / 10;
		if (cc.isGrounded) {
			movement.y = 0;
		}

		if (Input.GetButton("Jump") && cc.isGrounded) {
			Debug.Log ("skač pizdica");
			movement.y = jumpForce;
		}

		cc.Move (movement * Time.deltaTime);
		if (movement.z != 0) {
			// TODO: fix rotation
			cc.transform.rotation = Quaternion.Euler(new Vector3(0, 90 * moveDirection - 90, 0));
		}
    }

}
