using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach script to empty gameObject, set desired size of space in editor. #spacebois for lyfe.
//Gets pretty laggy after about 500 planets so check yo self.
public class SpawnerArea : MonoBehaviour {

	public GameObject[] PlanetToSpawn; 

	public int numPlanets = 100;

	public Vector3 center;
	public Vector3 size;

	// Update is called once per frame
	void Start () {           

		SpawnPlanets ();

	}
		
	public void SpawnPlanets() {

		for(int i = 0; i <= numPlanets; i++) {
	
		//gets random point within cube to instantiate gameobjects
		Vector3 pos = center + new Vector3 (Random.Range (-size.x / 2, size.x / 2), 
			                                Random.Range (-size.y / 2, size.y / 2), 
			                                Random.Range (-size.z / 2, size.z / 2));
		
		//chooses random planet within array to spawn at random position in cube
		Instantiate (PlanetToSpawn [Random.Range(0, PlanetToSpawn.Length)], pos, Quaternion.identity);
	
	    }
	}

	void OnDrawGizmosSelected() {
	   
		Gizmos.color = new Color (1, 0, 0, 0.5f);
		Gizmos.DrawCube (transform.localPosition + center, size);
	}

}
