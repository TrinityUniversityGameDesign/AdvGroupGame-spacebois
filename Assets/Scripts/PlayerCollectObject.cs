﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollectObject : MonoBehaviour {
    public float raycastDist = 5f;
    public int collectGoal = 5;
    // Use this for initialization
    void Start()
    {
        if (!GetComponent<PhotonView>().isMine)
        {
            Destroy(this);
        }
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;

        if (GetComponent<PhotonView>().isMine)
        {
            if(PhotonNetwork.player.GetScore() >= collectGoal)
            {
                Debug.Log("Goal Hit");
                ScoreContainer.scores = PhotonNetwork.playerList;
                GetComponent<PhotonView>().RPC("TransitionScreen", PhotonTargets.AllViaServer, null);
                PhotonNetwork.SendOutgoingCommands();
            }
            if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, raycastDist))
            {
                if (hit.transform.tag == "Collect")
                {
                    hit.transform.gameObject.GetComponent<diamondControl>().destroySelf();
                    PhotonNetwork.player.SetScore(PhotonNetwork.player.GetScore() + 1);

                    // cmw edited & tested
                    GetComponent<PlayerMovement>().speedExhaust = GetComponent<PlayerMovement>().speedExhaustScale;
                    Debug.LogError("CMW: " + GetComponent<PlayerMovement>().speedExhaust + " " + GetComponent<PlayerMovement>().speedExhaustScale);
                }
            }
        }  
    }

    [PunRPC]
    void TransitionScreen()
    {
        if(PhotonNetwork.isMasterClient){
            StartCoroutine(MasterClientSceneWait());
        }
        else{
            SceneManager.LoadScene("EndScene");
        }
    }

    IEnumerator MasterClientSceneWait(){
        Debug.Log("Waiting for All players to leave");
        yield return new WaitUntil(() => PhotonNetwork.playerList.Length == 1);
        Debug.Log("Server is now loading scene");
        SceneManager.LoadScene("EndScene");
    }
}
