using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public int numEnemies;
    public GameObject enemy;
    public List<GameObject> enemies; 
    public List<GameObject> players;
    public List<Tuple<int,GameObject>> playerIDs; 
    public int numPlayers;
    public Tuple<int,GameObject> rem;
    
    // Use this for initialization
    void Start()
    {
        enemies = new List<GameObject>();
        playerIDs = new List<Tuple<int,GameObject>>();
        numPlayers = 0;
    }

    public void OnJoinedRoom()
    {
       if (PhotonNetwork.isMasterClient)
        {
            enemies.Add(PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0));
            players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")); 
            GameObject pl = players[numPlayers];
            Tuple<int,GameObject> tup = new Tuple<int,GameObject>(1,pl);
            playerIDs.Add(tup);  
            numPlayers++;
            setEnemyPlayers();
        
        }   
        
    }

    public void setEnemyPlayers(){
        foreach ( GameObject en in enemies){
            en.GetComponent<EnemyAI>().SetPlayers(players,playerIDs);
        }
    }

    public void OnPhotonPlayerConnected(PhotonPlayer other){
        //Might need to just store all the new players
        if (PhotonNetwork.isMasterClient)
        {
            enemies.Add(PhotonNetwork.Instantiate(enemy.name, transform.position, Quaternion.identity, 0));  
            StartCoroutine(waitSpawnIn(other.ID));
        }


    }

  
    private IEnumerator waitSpawnIn(int pid){
        yield return new WaitForSecondsRealtime(5);
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")); 
        GameObject pl = players[numPlayers];
        Tuple<int,GameObject> tup = new Tuple<int,GameObject>(pid,pl);
        playerIDs.Add(tup);
        setEnemyPlayers();  
        numPlayers++;
    }

    private IEnumerator waitSpawnOut(int pid){
        yield return new WaitForSecondsRealtime(5);
        foreach(var tup in playerIDs){
            if(tup.First == pid){
                rem = tup;
            }
        }
        playerIDs.Remove(rem);
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player")); 
        setEnemyPlayers();
        numPlayers--;
    }


    public void OnPhotonPlayerDisconnected(PhotonPlayer other){
        if (PhotonNetwork.isMasterClient)
        {
            StartCoroutine(waitSpawnOut(other.ID));
        }
    }




}
