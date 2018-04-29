using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveStartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if(Input.touchCount == 1 || Input.GetKeyDown(KeyCode.S)){
				SceneManager.LoadScene ("GeneratedPlayerWorldTest");
			}

	}


}
