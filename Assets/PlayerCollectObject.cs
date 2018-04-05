using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectObject : MonoBehaviour {
    public float raycastDist = 5f;
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

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, raycastDist))
        {
            if(hit.transform.tag == "Collect")
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RequestOwnership();
                if (hit.transform.gameObject.GetComponent<PhotonView>().isMine)
                {
                    PhotonNetwork.Destroy(hit.transform.gameObject);
                    PhotonNetwork.player.SetScore(PhotonNetwork.player.GetScore() + 1);
                    
                }
            }
        }
    }
}
