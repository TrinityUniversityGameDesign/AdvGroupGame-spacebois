using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radarBallControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (GetComponent<MeshRenderer>().enabled && Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            GetComponent<LineRenderer>().enabled = true;
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, hit.point);
        }
	}
}
