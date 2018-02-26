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
    private float time;
    private float randomTime;

	public GameObject targetPlayer;

    void SetRandomTime() {
        randomTime = Random.Range(1, 5);
    }

	void Start () {
        time = 0;
        SetRandomTime();
		targetX = Random.Range (targetPlayer.transform.position.x - radius, targetPlayer.transform.position.x + radius);
		targetY = Random.Range (targetPlayer.transform.position.y - radius, targetPlayer.transform.position.y + radius);
		targetZ = Random.Range (targetPlayer.transform.position.z - radius, targetPlayer.transform.position.z + radius);

		targetTransform = new Vector3(targetX, targetY, targetZ);

		
	}
	
	// Update is called once per frame
	void Update () {


		float step = speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, targetTransform) < 2) {
            time += Time.deltaTime;
        }
        if(time > randomTime) {
            time = 0;
            SetRandomTime();
			targetX = Random.Range (targetPlayer.transform.position.x - radius, targetPlayer.transform.position.x + radius);
			targetY = Random.Range (targetPlayer.transform.position.y - radius, targetPlayer.transform.position.y + radius);
			targetZ = Random.Range (targetPlayer.transform.position.z - radius, targetPlayer.transform.position.z + radius);
			targetTransform = new Vector3(targetX, targetY, targetZ);
            transform.LookAt(targetTransform);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, targetTransform, step);
		}
	}
}
