using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectGen : MonoBehaviour {
    public GameObject o;
	// Use this for initialization
	void Start () {
		
	}

    public void OnJoinedRoom()
    {
        Vector3 spawnPos = Vector3.zero;

        Vector3 random = Random.insideUnitSphere;
        random.y = 0;
        random = random.normalized;
        Vector3 itempos = spawnPos + 10f * random;

        PhotonNetwork.Instantiate(o.name, itempos, Quaternion.identity, 0);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
