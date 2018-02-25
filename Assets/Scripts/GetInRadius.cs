using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInRadius : MonoBehaviour {

	private float targetX;
	private float targetY;
	private float targetZ;
	private Vector3 targetTransform;

	public float radius; 
	public float speed;
	public float randomWait;
	private float time;

	public GameObject targetPlayer;

	void SetRandomTime(){
        randomWait = Random.Range(3, 5);
	}

	// Use this for initialization
	void Start () {
		time = 0;
		SetRandomTime ();
		
		do {
			targetX = Random.Range(targetPlayer.transform.position.x - radius, targetPlayer.transform.position.x + radius);
			targetY = Random.Range(targetPlayer.transform.position.y - radius, targetPlayer.transform.position.y + radius);
			targetZ = Random.Range(targetPlayer.transform.position.z - radius, targetPlayer.transform.position.z + radius);
		} while (targetX * targetX + targetY * targetY + targetZ * targetZ > radius * radius);

		targetTransform = new Vector3(targetX, targetY, targetZ);

		
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		float step = speed * Time.deltaTime;

		if (Vector3.Distance (transform.position, targetTransform) < 2) {
			time += Time.deltaTime;
		}

		if(time > randomWait){
			do {
				targetX = Random.Range (targetPlayer.transform.position.x - radius, targetPlayer.transform.position.x + radius);
				targetY = Random.Range (targetPlayer.transform.position.y - radius, targetPlayer.transform.position.y + radius);
				targetZ = Random.Range (targetPlayer.transform.position.z - radius, targetPlayer.transform.position.z + radius);
			} while (targetX * targetX + targetY * targetY + targetZ * targetZ > radius * radius);
			targetTransform = new Vector3(targetX, targetY, targetZ);
			time = 0; 
			SetRandomTime ();
		} else {
			transform.position = Vector3.MoveTowards (transform.position, targetTransform, step);
		}
	}
}
