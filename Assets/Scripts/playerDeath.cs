using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDeath : MonoBehaviour {

	// Use this for initialization
	public void die(){
		//if we aren't the master client
		if (!PhotonNetwork.isMasterClient)
        {
          this.gameObject.tag = "Dead";
   		  SceneManager.LoadScene("DeadTest");
        }
        else {
        	//Maybe have a call to give the session to a new player? 
        	Debug.LogWarning("MASTER CLIENT PLAYER CALLING DEAD");
        	this.gameObject.tag = "Dead";
			//SceneManager.LoadScene("DeadTest");
        }
	}

}
