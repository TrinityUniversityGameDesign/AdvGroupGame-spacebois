using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// idea: make player movement based off where the camera is looking.
// idea: to speed up or slow down, use left and right trigger buttons

public class PlayerMovement : MonoBehaviour {

    public bool isMoving = true;
    public float speed = 1f;
    public bool keyDetection;
    public GameObject cockpit;
    public Quaternion targetRotation;
    public float turningRate = 1f;

    // Use this for initialization
    void Start () {
        if (Application.isEditor)
        {
            keyDetection = true;
        }
        targetRotation = Camera.main.transform.rotation;

    }
	
	// Update is called once per frame
	void Update () {
        if(isMoving){
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
            if (GetComponent<PhotonView>().isMine) targetRotation = Camera.main.transform.rotation;
        }
        if (GetComponent<PhotonView>().isMine)
        {
            cockpit.transform.rotation = Quaternion.RotateTowards(cockpit.transform.rotation, targetRotation, turningRate * Time.deltaTime);
            Camera.main.gameObject.transform.localPosition = Vector3.zero;
        }
        if (keyDetection){
            Debug.Log("Key detection active for editor");
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                if (speed > 0) speed -= 1f;
                else isMoving = false;
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow)){
                speed += 1f;
                isMoving = true;
            }
        } else {
            if (Input.touchCount == 1)
            {
                var touch = Input.touches[0];
                if (touch.position.x < Screen.width / 2)
                {
                    if (speed > 0) speed -= 1f;
                    else isMoving = false;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    speed += 1f;
                    isMoving = true;
                }
            }
        }
	}
}
