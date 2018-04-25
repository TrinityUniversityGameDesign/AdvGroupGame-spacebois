using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractiveStartButton : MonoBehaviour {

	private float timer;
	public float gazeTime = 2f;
	private bool gazedAt;
	public string theSceneName;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(gazedAt){
			timer += Time.deltaTime;

			if(timer >= gazeTime){
				SceneManager.LoadScene (theSceneName);
			}
		}
	}

	public void PointerEnter() {
		gazedAt = true;
	}

	public void PointerExit() {
		gazedAt = false;
	}
}
