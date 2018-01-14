using System;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed = 7.5f;
	public float JumpForce = 15f;
	public float Gravity = 6f;

	public AudioClip JumpSound;
	public AudioClip PickupSound;
	public AudioClip TurnOnSound;
	public AudioClip TurnOffSound;

	private Vector3 _movement = Vector3.zero;

	private CharacterController _cc;
	private Camera _oc;
	private Transform _po;
	private AudioSource _as;

	private GameManager _gameManager;

	private GameObject _uiOnPickup;

	public int Score = 0;
	public bool HasKey = false;

	private Animator _anim;

	// Use this for initialization
	void Start ()
	{
        _cc = GetComponent<CharacterController> ();
		_po = transform.Find("PlayerObject");
		_oc = transform.Find("OverviewCamera").GetComponent<Camera>();
		_as = GetComponent<AudioSource>();
		_anim = _po.GetComponent<Animator>();
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		_uiOnPickup = GameObject.Find("OnPickup");
	}

	private void FixedUpdate()
	{
		MovePlayer();
	}

	// Update is called once per frame
	void Update ()
	{
		// Kill player if it falls off
		if(_cc.transform.position.y < -20) _gameManager.DeathMsg("falling off into an endless pit");
		
		// If key for map is held, show overview camera
		if (Input.GetButton("Map"))
		{
			_oc.enabled = true;
		}
		else
		{
			_oc.enabled = false;
		}
	}
	
	private Vector3 handleKeyInput()
	{
		return Vector3.Normalize(new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")));
	}

	private void MovePlayer()
	{
		// Move on the given movement axis
		Vector3 moveDirection = handleKeyInput();
		moveDirection *= MoveSpeed;
		_movement.x = moveDirection.x;
		_movement.z = moveDirection.z;
		
		_anim.SetFloat("Speed", moveDirection.magnitude);
		
		// Restrict x movement between -5 and +5
		if (_cc.transform.position.x > +5 && _movement.x > 0) _movement.x = 0;
		if (_cc.transform.position.x < -5 && _movement.x < 0) _movement.x = 0;
		
		// Apply gravity if we are in the air
		if (!_cc.isGrounded)
		{
			_movement.y -= Gravity / 10;
		}
		else
		{
			_movement.y = -5;
		}

		// Apply jump force on jump click
		if (_cc.isGrounded)
		{
			_anim.SetTrigger("Grounded");
			_anim.ResetTrigger("Jump");
			if (Input.GetButton("Jump"))
			{
				_anim.SetTrigger("Jump");
				_as.PlayOneShot(JumpSound);
				_movement.y = JumpForce;
			}
		}
		else
		{
			_anim.ResetTrigger("Grounded");
		}

		// Apply all calculated movements above
		_cc.Move(_movement * Time.deltaTime);

		// Apply rotation if we are moving by at least the threshold below
		const float thr = 0.05f;
		if(Math.Abs(moveDirection.z) > thr || Math.Abs(moveDirection.x) > thr)
			_po.transform.rotation = Quaternion.LookRotation(moveDirection);
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Switch>())
		{
			Switch s = other.GetComponent<Switch>();
			bool newState = s.TurnSwitch();
			if(newState) _as.PlayOneShot(TurnOnSound);
			else _as.PlayOneShot(TurnOffSound);
		} else if (other.GetComponent<Pickup>())
		{
			other.gameObject.SetActive(false);
			_as.PlayOneShot(PickupSound);
			int giveScore = other.GetComponent<Pickup>().ScoreAmount;
			Score += giveScore;
			GameObject.Find("Score").GetComponent<Score>().setScore(Score);
			_uiOnPickup.GetComponent<Text>().text = "+" + giveScore;
			_uiOnPickup.GetComponent<Animator>().Play("Pickup", -1, 0f);
		} else if (other.GetComponent<Key>())
		{
			HasKey = true;
			other.gameObject.SetActive(false);
			_as.PlayOneShot(PickupSound);
			GameObject.Find("HasKeyUI").GetComponent<Animator>().Play("HasKey");
			_uiOnPickup.GetComponent<Text>().text = "Found the Key";
			_uiOnPickup.GetComponent<Animator>().Play("Pickup", -1, 0f);
		} else if (other.GetComponent<Spikes>())
		{
			_gameManager.DeathMsg("in a prickly pit of pointy spikes");
		} else if (other.GetComponent<Door>())
		{
			if (HasKey)
			{
				GameObject.Find("vrataopen").GetComponent<Animator>().Play("Blend Tree");
				Invoke("EndGame", 3f);
			}
			else
			{
				_uiOnPickup.GetComponent<Text>().text = "You don't have the Key";
				_uiOnPickup.GetComponent<Animator>().Play("Pickup", -1, 0f);
			}
		}
		else if (other.GetComponent<ZogaModraController>())
		{
			_gameManager.DeathMsg("rolled over by a nasty blue ball");
		}
	}

	private void EndGame()
	{
		_gameManager.WinMsg();
	}
}
