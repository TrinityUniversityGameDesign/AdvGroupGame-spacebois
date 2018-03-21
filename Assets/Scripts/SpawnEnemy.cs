using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int numEnemies;
    //CONSTANT MAXENEMIES
    // MAYBE CHANGE FROM ARRAYS TO KEEP FROM THIS?
    public int maxEnemies = 10; 
    public GameObject enemy;
    public GameObject[] enemies; 
    public GameObject[] players;
    public int[] playerIDs; 
   
   
    // Use this for initialization
    void Start()
    {
        numEnemies = 0;
        enemies = new GameObject[maxEnemies];
        playerIDs = new int[maxEnemies];
    }

    public void OnJoinedRoom()
    {
       if (PhotonNetwork.isMasterClient)
        {
            enemies[numEnemies] = PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);
            players = GameObject.FindGameObjectsWithTag("Player"); 
            playerIDs[numEnemies] = 1;
            enemies[numEnemies].GetComponent<EnemyAI>().SetPlayers(players,playerIDs);
            numEnemies++;
        }
        
    }
    public void OnPhotonPlayerConnected(PhotonPlayer other){
        //Might need to just store all the new players
        if (PhotonNetwork.isMasterClient)
        {
            enemies[numEnemies] = PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0);  
            StartCoroutine(waitSpawnIn(other.ID));
        }


    }

  
    private IEnumerator waitSpawnIn(int pid){
        yield return new WaitForSecondsRealtime(5);
        players = GameObject.FindGameObjectsWithTag("Player");
        playerIDs[numEnemies] = pid;
         foreach (GameObject en in enemies){
            if(en != null){
                    en.GetComponent<EnemyAI>().SetPlayers(players,playerIDs);
                }
            }
        numEnemies++;
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
