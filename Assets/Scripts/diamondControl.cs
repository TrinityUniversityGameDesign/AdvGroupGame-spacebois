using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diamondControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [PunRPC]
    public void destroySelf()
    {
        GetComponent<PhotonView>().RPC("destroySelf", PhotonTargets.OthersBuffered);
        Destroy(gameObject);
    }
}
