using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 7f;
	public float JumpForce = 18f;
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
			RotateByAxis(isMovingLeft, _movementAxis);			
		}
	}

	private void OnTriggerStay(Collider other)
	{
		// if other object is DisplacerObject 
		if (other.GetComponentInParent(typeof(DisplacerObject)) != null)
		{
			Transform displacerObject = other.gameObject.transform.parent;
			Transform target = displacerObject.Find("TargetArea");
			Transform source = displacerObject.Find("SourceArea");
			
			// If we are on source path (moving on Z axis) and inside SourceArea, we can press Fire3 (Left Shift) to
			// move into target path (moving on X axis).
			if (_movementAxis == 2 && other.transform.Equals(source))
			{
				if (Input.GetButtonDown("Fire3"))
				{
					DisplaceAction(target.GetComponent<BoxCollider>().bounds.center, 0);
				}
			}
			// If we are on target path (moving on X axis) and we touch SourceArea, we automatically jump back into
			// source path (moving on Z axis).
			else if (_movementAxis == 0 && other.transform.Equals(source))
			{
				DisplaceAction(source.GetComponent<BoxCollider>().bounds.center, 2); // start moving on Z axis
			}
			
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent(typeof(DisplacerCornerObject)) != null)
		{
			if(_movementAxis == 2) DisplaceAction(transform.position, 0);
			else if(_movementAxis == 0) DisplaceAction(transform.position, 2);
		}
	}

	private void DisplaceAction(Vector3 displacePosition, int displaceAxis)
	{
		// reset any currently ongoing movement
		_movement.x = 0;
		_movement.z = 0;
		// set position and new movement axis
		transform.position = displacePosition;
		_movementAxis = displaceAxis;
		// rotate camera
		if(displaceAxis == 2)
			transform.RotateAround(transform.position, Vector3.up, -90);
		else if(displaceAxis == 0)
			transform.RotateAround(transform.position, Vector3.up, +90);
	}

	// Rotate player object depending on left/right movement and displacement axis (if we are moving on Z or on X)
	private void RotateByAxis(bool movingToLeft, int displaceAxis)
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
