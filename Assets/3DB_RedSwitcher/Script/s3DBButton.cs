// The script is in an experimental stage. We welcome your suggestions and feedback to improve the functionality.
// Version 0.25 / Georg Zaim / 3DBakers 
using UnityEngine;

namespace s3DBakers.Buttons {
	[System.Serializable]
	public struct str3DdBbReceiver {
		public GameObject receiver;
		public enum3DBbState3 Switch;
		public str3DBbActions actions;

	}

	[System.Serializable]
	public struct str3DdBbReceiver2 {

		public GameObject receiver;
		[Space(10)]
		//[Header("Action:")]
		public bool position; 
		public bool rotation;  
		public bool objects;  
		public bool materials;
	}


	[System.Serializable]
	public struct str3DBbActions {
		public bool position; 
		public bool rotation;  
		public bool objects;  
		public bool materials;
	}

	[System.Serializable]
	public struct str3DBbScreenInfo {
		[Space(10)]
		[Tooltip("Show password symbols or dot instead of symbols")]
		public bool hidePassword;
		[Space(10)]
		public GameObject[] number1;
		public GameObject[] number2;
		public GameObject[] number3;
		public GameObject[] number4;
		[Space(10)]
		public GameObject[] dot1;
		public GameObject[] dot2;
		public GameObject[] dot3;
		public GameObject[] dot4;
		[Space(10)]
		public GameObject messageClosed;
		public GameObject messageOpened;
		//public GameObject message_Error;
	}

	[System.Serializable]
	public struct ste3DBbMats {
		public int materialID;
		public Material material;
	}

	public enum enum3dBbType {button,toggle}
	public enum enum3DBbState2 {turn0,turn1}
	public enum enum3DBbState3 {auto,turn0,turn1}
	public enum enum3DBbActions {auto,position,rotation,objects,materials}


	public struct str3DBbMessage {
		public GameObject GO;
		public enum3DBbState3 state;
		public str3DBbActions actions;
	}
}
