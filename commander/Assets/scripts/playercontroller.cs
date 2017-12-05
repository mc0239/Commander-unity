using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour {
    public float walkingSpeed = 5f;
    public float jumpForce = 3f;
    public float gravity = 10f;
    private Vector3 moveDir = Vector3.zero;
    private CharacterController cc;
    private Vector3 lastRotation=Vector3.zero;
	// Use this for initialization
	void Start () {
        cc = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        moveDir=handleKeyInput();
        if (cc.isGrounded) {
            if(!moveDir.Equals(Vector3.zero))transform.rotation = Quaternion.LookRotation(moveDir);
            moveDir = Vector3.Normalize(moveDir);
            moveDir *= walkingSpeed;
            if (Input.GetButtonDown("Jump")) {
                moveDir.y = jumpForce;
            }

        }

        moveDir.y -= gravity;
        Debug.Log(moveDir);
        cc.Move(moveDir * Time.deltaTime);
	}

    private Vector3 handleKeyInput()
    {
        if (-Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") > 0)
            return new Vector3(-1, 0, 1);
        if (-Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
            return new Vector3(-1, 0, 0);
        if (-Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") < 0)
            return new Vector3(-1, 0, -1);

        if (-Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") > 0)
            return new Vector3(0, 0, 1);
        if (-Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            return new Vector3(0, 0, 0);
        if (-Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") < 0)
            return new Vector3(0, 0, -1);

        if (-Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0)
            return new Vector3(1, 0, 1);
        if (-Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
            return new Vector3(1, 0, 0);
        if (-Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0)
            return new Vector3(1, 0, -1);

        else {
            Debug.Log("napaka pri smeri");
            return Vector3.zero;
        }
    }
}
