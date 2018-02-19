using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// idea: make player movement based off where the camera is looking.
// idea: to speed up or slow down, use left and right trigger buttons

public class PlayerMovement : MonoBehaviour {

    public bool isMoving = true;
    public float speed = 1f;
    public bool keyDetection;

	// Use this for initialization
	void Start () {
        if (Application.isEditor)
        {
            keyDetection = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if(isMoving){
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
        }

        if(keyDetection){
            Debug.Log("Key detection active for editor");
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                if (speed > 0) speed -= 1f;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)){
                speed += 1f;
            }
        } else {
            if (Input.touchCount == 1)
            {
                var touch = Input.touches[0];
                if (touch.position.x < Screen.width / 2)
                {
                    if (speed > 0) speed -= 1f;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    speed += 1f;
                }
            }
        }
	}
}
