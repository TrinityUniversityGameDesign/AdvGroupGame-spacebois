using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	public float speed; 
	public float radius; 

	public int numPlayers;
	enum EnemyState {Inactive,Search,Wander};
	EnemyState curState;
 
 	public Transform loc;

	void Start(){
		numPlayers = GameObject.FindGameObjectsWithTag("Player").Length;
		curState = EnemyState.Inactive;
	} 

	void Update(){
		switch (curState) 
		{
			case EnemyState.Inactive:
			  //Should maybe consider not having to store the number of players? 
			  if(numPlayers > 0){curState = EnemyState.Search;}
			  break;
			case EnemyState.Search:
			  FindPlayer();
			  break;
			case EnemyState.Wander:
			  GoToLocation();
			  break;
		}
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
				loc.gameObject.tag = "Dead";
				numPlayers -= 1;
				//Setting the state to Inactive, since we have killed our target. 
				curState = EnemyState.Inactive;
		}
	}

	public void FindPlayer(){

		GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
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