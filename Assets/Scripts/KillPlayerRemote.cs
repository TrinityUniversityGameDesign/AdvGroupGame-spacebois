using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KillPlayerRemote : MonoBehaviour
{
    
     public int sPlayer;

     void Awake() {
        sPlayer = PhotonNetwork.player.ID;
     }  


     [PunRPC]
     void setDead(int playerID)
     {
        Debug.LogWarning("o. RPC setDead");
        if(sPlayer == playerID){
            Debug.LogWarning("oo. this client setDead");
            this.tag = "Dead";
            //SceneManager.LoadScene("DeadTest");
        }
        Debug.LogWarning("ooo. RPC setDead Over");
    }

    public void killPlayer(int playerID){
        Debug.LogWarning("i. PLAYER CALLING KILL PLAYER");
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("setDead", PhotonTargets.All, playerID);
        Debug.LogWarning("ii. PLAYER CALLED KILL PLAYER");
    }

}