using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {
    public float walkingSpeed = 5f;
    public float jumpForce = 3f;
    public float gravity = 10f;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController cc;
	// Use this for initialization
	void Start () {
        cc = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (cc.isGrounded) {
            moveDir = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= walkingSpeed;
            if (Input.GetButtonDown("Jump")) {
                moveDir.y = jumpForce;
            }

        }

        moveDir.y -= gravity * Time.deltaTime;

        cc.Move(moveDir * Time.deltaTime);
	}
}
