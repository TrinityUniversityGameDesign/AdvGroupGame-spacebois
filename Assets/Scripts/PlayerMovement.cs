using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// idea: make player movement based off where the camera is looking.
// idea: to speed up or slow down, use left and right trigger buttons

public class PlayerMovement : MonoBehaviour {

    public bool isMoving = true;
    public float speed = 0f;
    public bool keyDetection;
    public GameObject cockpit;
    public Quaternion targetRotation;
    public float turningRate = 1f;

    public float speedExhaust; // 100% level is speedExhaustScale
    public float speedExhaustScale = 5000f;
    private bool exhausted = false;

    private GameObject worldInfo;
    private float xBound;
    private float yBound;
    private float zBound;

    // Use this for initialization
    void Start () {
        if (Application.isEditor)
        {
            keyDetection = true;
        }
        targetRotation = Camera.main.transform.rotation;
        speedExhaust = speedExhaustScale;
        worldInfo = GameObject.Find("PlayArea");
        xBound = (worldInfo.GetComponent<SpawnerArea>().size.x)/2 + 100;
        yBound = (worldInfo.GetComponent<SpawnerArea>().size.y) / 2 + 100;
        zBound = (worldInfo.GetComponent<SpawnerArea>().size.z) / 2 + 100;
        speed = 0; //start not moving
    }

    void boundCheck(){
        float x = transform.position.x;
        float y = transform.position.y;
        float z = transform.position.z;
        if (x > xBound) {
            transform.position = new Vector3(-xBound + 50, y, z);
        } else if (x < -xBound) {
            transform.position = new Vector3(xBound - 50, y, z);
        } else if (y > yBound) {
            transform.position = new Vector3(x, -yBound + 50, z);
        } else if(y < -yBound) {
            transform.position = new Vector3(x, yBound - 50, z);
        } else if(z > zBound){
            transform.position = new Vector3(x, y, -zBound + 50); 
        } else if (z < -zBound){
            transform.position = new Vector3(x, y, zBound - 50);
        } else {
           Debug.Log("In Bound");
        }
    }
	
	// Update is called once per frame
	void Update () {
        boundCheck();
        if (speedExhaust <= 0)
        {
            exhausted = true;
            speed = 0f;
        }
        else { exhausted = false;  }
        if(speedExhaust < speedExhaustScale) speedExhaust += 1f;
        if(isMoving && !exhausted){
            transform.position = transform.position + Camera.main.transform.forward * speed * Time.deltaTime;
            //Debug.Log("Moved with speed of " + speed);
            speedExhaust -= speed/10;
            if (GetComponent<PhotonView>().isMine) targetRotation = Camera.main.transform.rotation;
        }
        Debug.LogWarning("Speed Exhaust: " + speedExhaust);
        //if(speedExhaust < 0f) Debug.LogWarning("Exhausted");
        if (GetComponent<PhotonView>().isMine)
        {
            //cockpit.transform.rotation = Quaternion.Euler(Quaternion.RotateTowards(cockpit.transform.rotation, targetRotation, turningRate * Time.deltaTime) * (cockpit.transform.position - Camera.main.transform.forward) + Camera.main.transform.forward);
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

        /*if(speedExhaust < speedExhaustScale){
            speedExhaust += 1f;
        }*/

        //Debug.LogWarning(speedExhaust);
	}
}
