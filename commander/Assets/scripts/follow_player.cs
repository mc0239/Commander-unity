using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_player : MonoBehaviour {
    private Vector3 startPosition;
    public GameObject player;
    private Vector3 playerStartPosition;
	// Use this for initialization
	void Start () {
        startPosition = transform.position;
        playerStartPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 playerChange = player.transform.position-playerStartPosition;



        transform.position = startPosition + playerChange;
	}
}
