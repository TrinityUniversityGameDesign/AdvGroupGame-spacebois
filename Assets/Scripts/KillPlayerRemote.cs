using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class KillPlayerRemote : MonoBehaviour
{

	public GameObject ov;
    
    public int sPlayer;
	public bool hasBeenHit;

     void Awake() {
        sPlayer = PhotonNetwork.player.ID;
		ov = GameObject.Find ("Overlay");
     }  


     [PunRPC]
     void setDead(int playerID)
     {
        
        if(sPlayer == playerID){
			hasBeenHit = true;
            //this.tag = "Dead";
            
        }
       
    }

	void Update() {
		if (hasBeenHit && GetComponent<PhotonView>().isMine) {
			StartCoroutine ("Flash");
			gameObject.transform.position = new Vector3(0,0,0);
		}
	}

    public void killPlayer(int playerID){
       
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("setDead", PhotonTargets.All, playerID);
        
    }

	IEnumerator Flash() {
		ov.GetComponent<Image> ().enabled = true;
		yield return new WaitForSeconds (.1f);
		ov.GetComponent<Image> ().enabled = false;
		hasBeenHit = false;
	}

}