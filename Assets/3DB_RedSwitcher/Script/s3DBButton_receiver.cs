// The script is in an experimental stage. We welcome your suggestions and feedback to improve the functionality.
// version 0.25 / Georg Zaim / 3DBakers 

using UnityEngine;
using System.Collections;
using s3DBakers.Buttons;

public class s3DBButton_receiver : MonoBehaviour {
	
	public bool debug;

	[Header("Change local position")]
	public enum3dBbType position;
	public Vector3 position0;
	public Vector3 position1;

	[Header("Change local rotation")]
	public enum3dBbType rotation;
	public Vector3 rotation0;
	public Vector3 rotation1;

	[Header("To activate or deactivate objects")]
	public enum3dBbType objects;
	public GameObject[] objects0;
	public GameObject[] objects1;

	[Header("Change materials")] // For example, the materials the lights On and Off..
	public enum3dBbType materials;
	public ste3DBbMats[] materials0;
	public ste3DBbMats[] materials1;

//	[Header("Send message")] 
//	public GameObject SendTo0;
//	public string Message0;
//	public GameObject SendTo1;
//	public string Message1;

	bool statePosition; 
	bool stateRotation;
	bool stateObjects;
	bool stateMaterials;
	bool stateMessage;

	enum3DBbState3 state;

	str3DBbMessage msg;


	public void button (str3DBbMessage msg) {
		state = msg.state;

// Change local position
		if (msg.actions.position) {
			//sPosition = msg.state;
			switchPosition();
		}

// Change local rotation
		if (msg.actions.rotation) {
			switchRotation ();
		}

// To activate or deactivate objects (SetActive)
		if (msg.actions.objects) {
			switchObjects ();
		}

		// Change materials 
		if (msg.actions.materials) {
			switchMaterials ();
		}
	}
		

	void switchPosition(){
		//enum3DBbState3 state = sPosition;
		switch (state){
		case enum3DBbState3.turn0:
			this.transform.localPosition = position0;
			statePosition = false;
			break;
		case enum3DBbState3.turn1:
			this.transform.localPosition = position1;
			statePosition = true;
			break;
		case enum3DBbState3.auto: 
			if (statePosition) {
				goto case enum3DBbState3.turn0;
			} else {
				goto case enum3DBbState3.turn1;
			}
//			break;
		}

		if (position == enum3dBbType.button & statePosition) {
			Invoke ("switchPosition", 0.15f);
		}
	}

	void switchRotation(){
		//enum3DBbState3 state = sPosition;
		switch (state){
		case enum3DBbState3.turn0:
			this.transform.localRotation = Quaternion.Euler(rotation0.x, rotation0.y, rotation0.z);
			stateRotation = false;
			break;
		case enum3DBbState3.turn1:
			this.transform.localRotation = Quaternion.Euler(rotation1.x, rotation1.y, rotation1.z);
			stateRotation = true;
			break;
		case enum3DBbState3.auto: 
			if (stateRotation) {
				goto case enum3DBbState3.turn0;
			} else {
				goto case enum3DBbState3.turn1;
			}
			//			break;
		}

		if (rotation == enum3dBbType.button & stateRotation) {
			Invoke ("switchRotation", 0.15f);
		}
	}

	void switchObjects(){
		switch (state){
		case enum3DBbState3.turn0:
			setObjects (true,false);
			stateObjects = false;
			break;
		case enum3DBbState3.turn1:
			setObjects (false,true);
			stateObjects = true;
			break;
		case enum3DBbState3.auto: 
			if (stateObjects) {
				goto case enum3DBbState3.turn0;
			} else {
				goto case enum3DBbState3.turn1;
			}
			//			break;
		}

		if (objects == enum3dBbType.button & stateObjects) {
			Invoke ("switchObjects", 0.15f);
		}
	}

	void switchMaterials(){
		switch (state){
		case enum3DBbState3.turn0:
			setMaterials (materials0);
			stateMaterials = false;
			break;
		case enum3DBbState3.turn1:
			setMaterials (materials1);
			stateMaterials = true;
			break;
		case enum3DBbState3.auto: 
			if (stateMaterials) {
				goto case enum3DBbState3.turn0;
			} else {
				goto case enum3DBbState3.turn1;
			}
			//			break;
		}

		if (materials == enum3dBbType.button & stateMaterials) {
			Invoke ("switchMaterials", 0.15f);
		}
	}

	void setMaterials (ste3DBbMats[] m){
		Renderer rendr = this.GetComponent<Renderer> (); 
		Material[] rMats = rendr.materials;

		if(rendr != null){
			for (int c = 0; c < m.Length; c++) {
				ste3DBbMats r = m [c];
				rMats [r.materialID] = r.material;
				rendr.materials = rMats;
			}
		}
	}

	void setObjects(bool a, bool b){
		for (int c = 0; c < objects0.Length; c++) {
			GameObject r = objects0 [c];
			r.SetActive (a);
		}
		for (int c = 0; c < objects1.Length; c++) {
			GameObject r = objects1 [c];
			r.SetActive (b);
		}
	}

//	void switchPosition (){
//		if (statePosition) {
//			this.transform.localPosition = position0;
//			statePosition = false;
//		} else {
//			this.transform.localPosition = position1;
//			statePosition = true;
//		}
//		if (position == enum3dBbType.button & statePosition) {
//			Invoke ("switchPosition", 0.15f);
//		}
//	}

//	void switchRotation (){
//		if (stateRotation) {
//			this.transform.localRotation = Quaternion.Euler(rotation0.x, rotation0.y, rotation0.z);
//			stateRotation = false;
//		} else {
//			this.transform.localRotation = Quaternion.Euler(rotation1.x, rotation1.y, rotation1.z);
//			stateRotation = true;
//		}
//		if (rotation == enum3dBbType.button & stateRotation) {
//			Invoke ("switchRotation", 0.15f);
//		}
//	}

//	void switchObjects (){
//		if (stateObjects == true) {
//
//			for (int c = 0; c < objects0.Length; c++) {
//				GameObject r = objects0 [c];
//				r.SetActive (true);
//			}
//			for (int c = 0; c < objects1.Length; c++) {
//				GameObject r = objects1 [c];
//				r.SetActive (false);
//			}
//			stateObjects = true;
//
//		} else {
//
//			for (int c = 0; c < objects0.Length; c++) {
//				GameObject r = objects0 [c];
//				r.SetActive (false);
//			}
//			for (int c = 0; c < objects1.Length; c++) {
//				GameObject r = objects1 [c];
//				r.SetActive (true);
//			}
//			stateObjects = false;
//
//		}
//		if (objects == enum3dBbType.button & stateObjects) {
//			Invoke ("switchObjects", 0.15f);
//		}
//	}

//	void switchMaterials (){
//		Renderer rendr = this.GetComponent<Renderer> (); 
//		Material[] rMats = rendr.materials;
//
//		if (stateMaterials) {
//			for (int c = 0; c < materials0.Length; c++) {
//				ste3DBbMats r = materials0 [c];
//				rMats [r.materialID] = r.material;
//				rendr.materials = rMats;
//			}
//			stateMaterials = false;
//
//		} else {
//			for (int c = 0; c < materials1.Length; c++) {
//				ste3DBbMats r = materials1 [c];
//				rMats [r.materialID] = r.material;
//				rendr.materials = rMats;
//			}
//			stateMaterials = true;
//
//		}
//		if (materials == enum3dBbType.button & stateMaterials) {
//
//			Invoke ("switchMaterials", 0.15f);
//		}
//	}
		
}
