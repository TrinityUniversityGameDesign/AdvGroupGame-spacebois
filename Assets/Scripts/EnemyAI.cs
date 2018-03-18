using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public float speed; 
	public float radius; 

	enum EnemyState {Inactive,Search,Wander};
	EnemyState curState;
 
 	public Transform loc;
    public GameObject[] players;

	void Start(){
		curState = EnemyState.Inactive;
        players = GameObject.FindGameObjectsWithTag("Player");
	} 

	void Update(){
        if (GetComponent<PhotonView>().isMine)
        {
            switch (curState)
            {
                case EnemyState.Inactive:
                    //Should maybe consider not having to store the number of players? 
                    if (players.Length > 0) { curState = EnemyState.Search; }
                    else {  StartCoroutine(waitSearch());  }
                    break;
                case EnemyState.Search:
                    FindPlayer();
                    break;
                case EnemyState.Wander:
                    GoToLocation();
                    break;
            }
        }
	}

    //could probably just change to an update in player instantiation?
    private IEnumerator waitSearch(){
        yield return new WaitForSecondsRealtime(1);
        players = GameObject.FindGameObjectsWithTag("Player");
    }

	//Maybe have this take in the argument Transform loc?
	public void GoToLocation(){
		if(Vector3.Distance(transform.position,loc.position) > radius){			
				//Should this happen every call? 
				transform.LookAt(loc);
				//This definitely should be.
				float step = speed * Time.deltaTime;
				transform.position = Vector3.MoveTowards(transform.position,loc.position,step);
				//Debug.Log(transform.position);
			}
		else{
				//This would mean distance < 2
				//Player dead maybe? 
				//numPlayers -= 1;
				//Setting the state to Inactive, since we have killed our target. 
                loc.gameObject.GetComponent<playerDeath>().die();
				curState = EnemyState.Inactive;
                //Might want to change from array to just have the removal of the dead player?
                players = new GameObject[0]; 
            /*
             * cmw adding game ending transition
             */
            /*
             *   bjo changing game ending transistion to occur in player
             */
            Debug.LogWarning("ENEMY CALLING DEAD");
		}
	}

	public void FindPlayer(){

		//GameObject[] players;
        //players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject player in players)
        {
            Vector3 diff = player.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if ((curDistance < distance) && curDistance > 1)
            {
                closest = player;
                distance = curDistance;
            }
        }
        loc = closest.transform;
        //Setting the state to follow after; 
        curState = EnemyState.Wander;
	}


}