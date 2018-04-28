using System.Collections;
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
                GetComponent<PhotonView>().RPC("TransitionScreen", PhotonTargets.AllBuffered, null);
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
        SceneManager.LoadScene("EndScene");
    }
}
