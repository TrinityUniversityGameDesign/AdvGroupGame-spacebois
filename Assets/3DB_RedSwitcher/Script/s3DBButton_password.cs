// The script is in an experimental stage. We welcome your suggestions and feedback to improve the functionality.
// Version 0.25 / Georg Zaim / 3DBakers 


using UnityEngine;
using System.Collections;
using s3DBakers.Buttons;

public class s3DBButton_password : MonoBehaviour {
	
	[Space(10)]
	[Tooltip("The 'Secret' is a maximum of four characters and only numbers of 0-9 (for example, 0000 or 9999)")]
	public string secret;

	[Space(10)]
	[Tooltip("show debug log")]
	public bool debug;
	public GameObject button0;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;
	public GameObject button5;
	public GameObject button6;
	public GameObject button7;
	public GameObject button8;
	public GameObject button9;
	public GameObject generalButton;

	[Space(10)]
	public str3DBbScreenInfo informer;
	public str3DdBbReceiver2[] sendToOpen;

	GameObject[] buttons = new GameObject[10];
	bool iWait;
	string nSecret = "";

	void Awake(){
		// set array of buttons
		for (int b = 0; b < buttons.Length; b++) {
			string s = "button"+b;
			GameObject obj = (GameObject)this.GetType ().GetField (s).GetValue (this);
			if(obj!=null){
				buttons [b] = obj;
			} else Debug.LogError ("GameObject "+this.gameObject.name+ " : "+s+" == Null");

		}
		//test link
		if (generalButton == null) {
			Debug.LogError ("GameObject "+this.gameObject.name+" : General Button == Null");
		}
	}

	bool testSecret (){
		for (int s = secret.Length; s < 4; s++){ 	// выравнил длину секрета до 4х
			secret +="0";
		}

		secret = secret.Substring(0, 4);
		bool b = secret == nSecret;

		return b;
	}

	public void button (str3DBbMessage msg){
		bool s = testSecret ();
		secret = secret.Substring(0,4);

		if (msg.GO == generalButton & !iWait) {
			StartCoroutine (animInformer(s));
			Locker ();

		} else if (nSecret.Length < secret.Length  & !iWait) {

			for (int b = 0; b < buttons.Length; b++) {
				if (buttons [b] == msg.GO) {
					nSecret = nSecret + b.ToString();
					informerUpdate (nSecret.Length, b);
				}
			}

			if (nSecret.Length == secret.Length & !iWait & generalButton == null) { 
				StartCoroutine (animInformer(s));
				Locker ();
			}
		}
	}

	void Locker(){
		bool s = testSecret ();

		for (int a = 0; a < sendToOpen.Length; a++) {
			if (sendToOpen[a].receiver != null) {
				str3DBbMessage msg;
				msg.GO = this.gameObject;

				msg.actions.position = sendToOpen[a].position;
				msg.actions.rotation = sendToOpen[a].rotation;
				msg.actions.objects = sendToOpen[a].objects;
				msg.actions.materials = sendToOpen[a].materials;

				if (s) {
					msg.state = enum3DBbState3.turn1; 
				} else {
					msg.state = enum3DBbState3.turn0; 
				}

				if (debug)
					Debug.Log ("Password-Locker: " + s);

				sendToOpen[a].receiver.SendMessage ("button", msg);
			}
		}
	}	
// Обновить информер
	void informerUpdate(int length, int sybol){
		
		if (length == 1) {
			informer.dot1[0].SetActive (false);
			informer.dot2[0].SetActive (true);
			informer.dot3[0].SetActive (true);
			informer.dot4[0].SetActive (true);
			if (!informer.hidePassword) {
				for (int c = 0; c < informer.number1.Length; c++) {
					if (c == sybol) {
						informer.number1 [sybol].SetActive (true);
					} else informer.number1 [c].SetActive (false);
				}
			} else {
				for (int c = 0; c < informer.number1.Length; c++) informer.number1 [c].SetActive (false);
				informer.dot1[1].SetActive (true);
				informer.dot2[1].SetActive (false);
				informer.dot3[1].SetActive (false);
				informer.dot4[1].SetActive (false);
			}
		}

		if (length == 2) {
			informer.dot1[0].SetActive (false);
			informer.dot2[0].SetActive (false);
			informer.dot3[0].SetActive (true);
			informer.dot4[0].SetActive (true);
			if (!informer.hidePassword) {
				for (int c = 0; c < informer.number2.Length; c++) {
					if (c == sybol) {
						informer.number2 [sybol].SetActive (true);
					} else informer.number2 [c].SetActive (false);
				}
			} else {
				for (int c = 0; c < informer.number2.Length; c++) informer.number2 [c].SetActive (false);
				informer.dot1[1].SetActive (true);
				informer.dot2[1].SetActive (true);
				informer.dot3[1].SetActive (false);
				informer.dot4[1].SetActive (false);
			}
		}

		if (length == 3) {
			informer.dot1[0].SetActive (false);
			informer.dot2[0].SetActive (false);
			informer.dot3[0].SetActive (false);
			informer.dot4[0].SetActive (true);
			if (!informer.hidePassword) {
				for (int c = 0; c < informer.number3.Length; c++) {
					if (c == sybol) {
						informer.number3 [sybol].SetActive (true);
					} else informer.number3 [c].SetActive (false);
				}
			} else {
				for (int c = 0; c < informer.number3.Length; c++) informer.number3 [c].SetActive (false);
				informer.dot1[1].SetActive (true);
				informer.dot2[1].SetActive (true);
				informer.dot3[1].SetActive (true);
				informer.dot4[1].SetActive (false);
			}
		}
		if (length == 4) {
			informer.dot1[0].SetActive (false);
			informer.dot2[0].SetActive (false);
			informer.dot3[0].SetActive (false);
			informer.dot4[0].SetActive (false);
			if (!informer.hidePassword) {
				for (int c = 0; c < informer.number4.Length; c++) {
					if (c == sybol) {
						informer.number4 [sybol].SetActive (true);
					} else informer.number4 [c].SetActive (false);
				}
			} else {
				for (int c = 0; c < informer.number4.Length; c++) informer.number4 [c].SetActive (false);
				informer.dot1[1].SetActive (true);
				informer.dot2[1].SetActive (true);
				informer.dot3[1].SetActive (true);
				informer.dot4[1].SetActive (true);
			}
		}
	}

// Анимация сообщений
	IEnumerator animInformer (bool b){
		iWait = true;

		if (!generalButton) yield return new WaitForSeconds (0.5f);

		showElements (false,false,false);

		if (b) {
			informer.messageOpened.SetActive (true);
			informer.messageClosed.SetActive (false);

		} else {
			informer.messageOpened.SetActive (false);
			informer.messageClosed.SetActive (true);

		}

		yield return new WaitForSeconds (1.5f);
		informer.messageClosed.SetActive (false);
		informer.messageOpened.SetActive (false);
		informer.dot1 [0].SetActive (true);
		informer.dot2 [0].SetActive (true);
		informer.dot3 [0].SetActive (true);
		informer.dot4 [0].SetActive (true);


		nSecret = "";
		iWait = false;
	}

	void showElements (bool n, bool d, bool m){
		for (int c = 0; c < informer.number1.Length; c++) informer.number1 [c].SetActive (n);
		for (int c = 0; c < informer.number2.Length; c++) informer.number2 [c].SetActive (n);
		for (int c = 0; c < informer.number3.Length; c++) informer.number3 [c].SetActive (n);
		for (int c = 0; c < informer.number4.Length; c++) informer.number4 [c].SetActive (n);

		for (int c = 0; c < informer.dot1.Length; c++) informer.dot1 [c].SetActive (d);
		for (int c = 0; c < informer.dot2.Length; c++) informer.dot2 [c].SetActive (d);
		for (int c = 0; c < informer.dot3.Length; c++) informer.dot3 [c].SetActive (d);
		for (int c = 0; c < informer.dot4.Length; c++) informer.dot4 [c].SetActive (d);

		informer.messageOpened.SetActive (m);
		informer.messageClosed.SetActive (m);
	}


}



