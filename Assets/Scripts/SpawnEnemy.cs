using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnJoinedRoom()
    {
        
       if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);
        }

    }
    public void OnPhotonPlayerConnected(PhotonPlayer other){

        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);
        }

    }
}
