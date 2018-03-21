using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int numEnemies;
    public int maxEnemies = 10; 
    public GameObject enemy;
    public GameObject[] enemies; 
    public GameObject[] players;

   
    // Use this for initialization
    void Start()
    {
        numEnemies = 0;
        enemies = new GameObject[maxEnemies];
    }

    public void OnJoinedRoom()
    {
       if (PhotonNetwork.isMasterClient)
        {
            enemies[numEnemies] = PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);
            players = GameObject.FindGameObjectsWithTag("Player"); 
            enemies[numEnemies].GetComponent<EnemyAI>().SetPlayers(players);
            numEnemies++;
        }
        
    }
    public void OnPhotonPlayerConnected(PhotonPlayer other){
        if (PhotonNetwork.isMasterClient)
        {
            enemies[numEnemies] = PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);  
            numEnemies++;
            StartCoroutine(waitSpawnIn());
        }


    }

  
    private IEnumerator waitSpawnIn(){
        yield return new WaitForSecondsRealtime(4);
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject en in enemies){
            if(en != null){
                    en.GetComponent<EnemyAI>().SetPlayers(players);
                }
            }
    }


    public void OnPhotonPlayerDisconnected(PhotonPlayer other){
        if (PhotonNetwork.isMasterClient)
        {
            foreach (GameObject en in enemies){
                if(en != null){
                    en.GetComponent<EnemyAI>().UpdateState();
                }
            }
        }

    }


}
