using System;
using UnityEngine;

public class Switch : MonoBehaviour
{
	public Boolean SwitchState = false;
	public Material MaterialOn;
	public Material MaterialOff;
	public GameObject Gate;

	private Light _light;
	private GameObject _openable;
	private MeshRenderer _mr;
	
	private void Start()
	{
		_light = transform.Find("PointLight").GetComponent<Light>();
		_openable = Gate.transform.Find("GateOpenable").gameObject;
		_mr = GetComponent<MeshRenderer>();
		SetState(SwitchState);
	}

	public void TurnSwitch()
	{
		SwitchState = !SwitchState;
		SetState(SwitchState);
	}

	private void SetState(bool state)
	{
		if (state)
		{
			_mr.material = MaterialOn;
			_light.color = Color.green;
			_openable.SetActive(false);
		}
		else
		{
			_mr.material = MaterialOff;
			_light.color = Color.red;
			_openable.SetActive(true);
		}
	}
}
