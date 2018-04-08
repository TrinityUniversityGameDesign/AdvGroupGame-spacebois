using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectGen : MonoBehaviour {
    public GameObject o;
    public float spawnFreq = 5f;
    public float currTimer = 0f;
	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if(currTimer > spawnFreq)
            {
                PhotonNetwork.Instantiate(o.name, Random.insideUnitSphere * 1000f, Quaternion.identity, 0);
                currTimer = 0;
            }
            currTimer += Time.deltaTime;
        }
	}
}
