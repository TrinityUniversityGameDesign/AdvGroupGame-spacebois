using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassControl : MonoBehaviour {
    GameObject home;
	// Use this for initialization
	void Start () {
        home = GameObject.FindGameObjectWithTag("Home");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(home.transform);
	}
}
