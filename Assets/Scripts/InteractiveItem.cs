using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveItem : MonoBehaviour {

    private float timer;
    public float gazeTime = 2f;
    private bool gazedAt;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gazedAt){
            timer += Time.deltaTime;

            if(timer >= gazeTime){
                GetComponent<Rigidbody>().useGravity = true;
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
