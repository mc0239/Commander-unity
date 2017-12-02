using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro_camera_bobbing : MonoBehaviour {

    float original;
    public float bob = 70;
    public float speed = 1;
    void Start()
    {
        original = -40;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0,Mathf.Sin(Time.time/speed) * bob + original, 0);
    }
}
