using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNetworking : MonoBehaviour {
    public GameObject cockpit;
    public GameObject spaceship;
    public GameObject overlay;
	// Use this for initialization
	void Start () {
        if (!GetComponent<PhotonView>().isMine)
        {
            GetComponent<PlayerMovement>().enabled = false;
            //GetComponent<EditorCameraMovement>().enabled = false;
            //GetComponent<Camera>().enabled = false;
            transform.GetChild(1).GetComponent<Camera>().enabled = false;
            GameObject.Destroy(cockpit);
        }
        else
        {
            Destroy(spaceship);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
