using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour {

	public GameObject[] asteroids;
	public Vector3 spawnValues;
	public float spawnWait;
	public int startWait;
	public float spawnMostWait;
	public float spawnLeastWait;

	int randAsteroid;

	// Use this for initialization
	void Start () {
		StartCoroutine (Spawner());
	}
	
	// Update is called once per frame
	void Update () {
		spawnWait = Random.Range (spawnLeastWait, spawnMostWait);
	}

	IEnumerator Spawner () {
		yield return new WaitForSeconds (startWait);

		while (true) {

			randAsteroid = Random.Range (0, 5);

			Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), 1, Random.Range (-spawnValues.z, spawnValues.z));
			Vector3 randDir = new Vector3 (Random.value, Random.value, Random.value);

			Instantiate (asteroids [randAsteroid], spawnPosition, Quaternion.identity);

			transform.Rotate (randDir);

			yield return new WaitForSeconds (spawnWait);

		}
	}
}
