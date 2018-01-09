using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 7f;
	public float JumpForce = 15f;
	public float Gravity = 6f;

	private Vector3 _movement = Vector3.zero;

	private CharacterController _cc;
	private Transform _po;

	// Use this for initialization
	void Start ()
	{
        _cc = GetComponent<CharacterController> ();
		_po = transform.Find("PlayerObject");
	}

	// Update is called once per frame
	void Update ()
	{
		// Move on the given movement axis
		Vector3 moveDirection = handleKeyInput();
		moveDirection *= MoveSpeed;
		_movement.x = moveDirection.x;
		_movement.z = moveDirection.z;
		
		// Restrict x movement between -5 and +5
		if (_cc.transform.position.x > +5 && _movement.x > 0) _movement.x = 0;
		if (_cc.transform.position.x < -5 && _movement.x < 0) _movement.x = 0;
		
		// Kill player if it falls off
		if(_cc.transform.position.y < -20) PlayerKill();

		// Apply gravity if we are in the air
		if (!_cc.isGrounded) {
			_movement.y -= Gravity / 10;
		}

		// Apply jump force on jump click
		if (Input.GetButton("Jump") && _cc.isGrounded) {
			_movement.y = JumpForce;
		}

		// Apply all calculated movements above
		_cc.Move(_movement * Time.deltaTime);

		// Apply rotation if we are moving by at least the threshold below
		float thr = 0.05f;
		if(Math.Abs(moveDirection.z) > thr || Math.Abs(moveDirection.x) > thr)
		_po.transform.rotation = Quaternion.LookRotation(moveDirection);
	}
	
	private Vector3 handleKeyInput()
	{
		return Vector3.Normalize(new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")));
	}

	public void PlayerKill()
	{
		_cc.transform.position = Vector3.zero;
	}
}
