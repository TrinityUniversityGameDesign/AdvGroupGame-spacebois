// The script is in an experimental stage. We welcome your suggestions and feedback to improve the functionality.
// version 0.25 / Georg Zaim / 3DBakers

using UnityEngine;
using System.Collections;
using s3DBakers.Buttons;

public class s3DBButton_sender : MonoBehaviour {
	
	public str3DdBbReceiver[] SendToGameObjects;

	public void SendToObjects(){
		str3DBbMessage msg;
		msg.GO = this.gameObject;

		for (int c = 0; c < SendToGameObjects.Length; c++) {

			msg.actions = SendToGameObjects [c].actions;
			msg.state = SendToGameObjects [c].Switch;

			SendToGameObjects [c].receiver.SendMessage ("button", msg);
				
			}
	}

//Just for test
	void OnMouseOver(){
		if (Input.GetMouseButtonDown (0)) {
			SendToObjects ();
		}
	}

}
