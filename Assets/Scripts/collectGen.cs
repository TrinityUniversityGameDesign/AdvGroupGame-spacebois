using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectGen : MonoBehaviour {
    public GameObject o;
    public float spawnFreq = 5f;
    public float currTimer = 0f;
    public Vector3 spawnPos;
    public GameObject enemSpawn;
    public SpawnEnemy spawnScript;
    private GameObject enemy;

	// Use this for initialization
	void Start () {
        enemSpawn = GameObject.Find("EnemySpawn");
        spawnScript = enemSpawn.GetComponent<SpawnEnemy>();
        enemy = spawnScript.enemy;
    }

    // Update is called once per frame
    void Update () {
        if (PhotonNetwork.isMasterClient)
        {
            if(currTimer > spawnFreq)
            {
                spawnPos = Random.insideUnitSphere * 1000f;
                PhotonNetwork.Instantiate(o.name, spawnPos, Quaternion.identity, 0);
                spawnScript.enemies.Add(PhotonNetwork.Instantiate(enemy.name, spawnPos + Random.insideUnitSphere * 50f, Quaternion.identity, 0));
                spawnScript.setEnemyPlayers();
                currTimer = 0;
            }
            currTimer += Time.deltaTime;
        }
	}
}
