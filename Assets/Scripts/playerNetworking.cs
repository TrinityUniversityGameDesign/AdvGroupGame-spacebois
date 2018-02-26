using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerNetworking : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!GetComponent<PhotonView>().isMine)
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<EditorCameraMovement>().enabled = false;
            //GetComponent<Camera>().enabled = false;
            transform.GetChild(0).GetComponent<Camera>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
