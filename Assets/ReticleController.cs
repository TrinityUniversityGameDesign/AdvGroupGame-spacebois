using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Image>().rectTransform.rotation = Quaternion.LookRotation(GetComponent<Image>().rectTransform.forward, Vector3.up);
	}
}
