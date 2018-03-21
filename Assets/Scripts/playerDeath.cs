using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour {

	// Use this for initialization
	public void Update(){
		if (this.gameObject.tag == "Dead" && !PhotonNetwork.isMasterClient)
        {
   		  SceneManager.LoadScene("DeadTest");
        }
       
	}

}
