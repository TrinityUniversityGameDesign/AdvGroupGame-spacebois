using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			GetComponent<Rigidbody> ().AddForce (transform.forward*10);
		} 
		if (Input.GetKey(KeyCode.DownArrow)) {
			GetComponent<Rigidbody> ().AddForce (transform.forward*-10);
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
			GetComponent<Rigidbody> ().AddForce (transform.right * 10);
		}
		if(Input.GetKey(KeyCode.LeftArrow)) {
			GetComponent<Rigidbody> ().AddForce (transform.right * -10);
		}
	}
}
