using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCollection : MonoBehaviour {

	private float timer;
	public float gazeTime = 2f;
	private bool gazedAt;
	private float score;
	private GameObject gm;
	private float x, y, z;


	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("diamond");
	}

	// Update is called once per frame
	void Update () {
		if(gazedAt){
			timer += Time.deltaTime;

			if(timer >= gazeTime){
				gm.SetActive (false);
				score += 1;
				x = Random.Range (0, 100);
				y = Random.Range (0, 100);
				z = Random.Range (0, 100);
				gm.transform.position = new Vector3 (x, y, z);
				gm.SetActive (true);
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