using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// idea: make player movement based off where the camera is looking.
// idea: to speed up or slow down, use left and right trigger buttons

public class PlayerMovement : MonoBehaviour {

    public bool isMoving = true;
    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isMoving){
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }
		
	}
}
