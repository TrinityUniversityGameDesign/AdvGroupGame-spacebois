using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int numEnemies;
    public int maxEnemies = 10; 
    public GameObject enemy;
    public GameObject[] enemies; 

    // Use this for initialization
    void Start()
    {
        numEnemies = 0;
        enemies = new GameObject[maxEnemies];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnJoinedRoom()
    {
       if (PhotonNetwork.isMasterClient)
        {
            enemies[numEnemies] = PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);
            numEnemies++;
        }

    }
    public void OnPhotonPlayerConnected(PhotonPlayer other){

        if (PhotonNetwork.isMasterClient)
        {
            enemies[numEnemies] = PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);  
            numEnemies++;

            foreach (GameObject e in enemies){
                e.GetComponent<EnemyAI>().UpdateState();
            }
        }


    }
}
