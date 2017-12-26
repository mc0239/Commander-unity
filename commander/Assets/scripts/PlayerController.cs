using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 7f;
	public float JumpForce = 15f;
	public float Gravity = 6f;

	private int _movementAxis = 2; // by default, move by Z axis

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
		float moveDirection = Input.GetAxis ("Horizontal");
		_movement[_movementAxis] = moveDirection * MoveSpeed;

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
		if (Math.Abs(_movement[_movementAxis]) > 0.05f)
		{
			// if we are moving to left (negative movement axis value)
			bool isMovingLeft = (_movement[_movementAxis] < 0);
			RotatePlayerModelByAxis(isMovingLeft, _movementAxis);			
		}
	}

	private void OnTriggerStay(Collider other)
	{
		// if other object is DisplacerObject 
		if (other.GetComponentInParent(typeof(RotatorObject)) != null)
		{
			if (Input.GetButtonDown("Fire3"))
			{
				Vector3 cPos = other.bounds.center;
				if (_movementAxis == 2) PerformAxisRotation(cPos, 0);
				else if(_movementAxis == 0) PerformAxisRotation(cPos, 2);
			}
		}
	}

	private void PerformAxisRotation(Vector3 rotatorPosition, int newAxis)
	{
		Debug.Log("New movement axis: " + newAxis);
		// reset any currently ongoing movement
		_movement.x = 0;
		_movement.z = 0;
		// set movement axis and reset position to center
		transform.position = rotatorPosition;
		_movementAxis = newAxis;
		// rotate camera
		switch (_movementAxis)
		{
			case 2:
				transform.RotateAround(transform.position, Vector3.up, -90);
				break;
			case 0:
				transform.RotateAround(transform.position, Vector3.up, 90);
				break;
			default:
				Debug.LogError("newAxis argument must be 0 or 2!");
				break;
		}
	}

	// Rotate player object depending on left/right movement and displacement axis (if we are moving on Z or on X)
	private void RotatePlayerModelByAxis(bool movingToLeft, int displaceAxis)
	{
		int mtl;
		if (movingToLeft) mtl = 1;
		else mtl = 0;
		
		if (displaceAxis == 0)
		{
			if (mtl == 0) mtl = -1;
			_po.transform.rotation = Quaternion.AngleAxis(-90 * mtl, Vector3.up);
		}
		else if (displaceAxis == 2)
		{
			_po.transform.rotation = Quaternion.AngleAxis(180 * mtl, Vector3.up);
		}
		else
		{
			Debug.Log("Invalid displaceAxis specified.");
		}
	}
	
}
