using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class enemyUIHit : MonoBehaviour {
	public GameObject gm;
	public GameObject ov;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("PlayerContainerNetworked");



	}

	// Update is called once per frame
	void Update () {
		if (gm.GetComponent<KillPlayerRemote> ().hasBeenHit) {
			//flash red a few times
			StartCoroutine("Flash");
			gm.transform.position = new Vector3(0,0,0);

		}
	}

	IEnumerator Flash() {
		ov.GetComponent<Image> ().enabled = true;
		yield return new WaitForSeconds (.1f);
		ov.GetComponent<Image> ().enabled = false;


	}
}
