using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarController : MonoBehaviour {
    List<GameObject> planetSprites = new List<GameObject>();
    public GameObject planetSprite;
    int numPlanets;
    public float distance = 100f;
    // Use this for initialization
    void Start () {
        numPlanets = GameObject.FindGameObjectsWithTag("Planet").Length;
        for (int i = 0; i < numPlanets; i++)
        {
            planetSprites.Add(Instantiate(planetSprite, transform));
            planetSprites[i].GetComponent<MeshRenderer>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        //transform.rotation = Quaternion.identity;
        int planetCount = 0;
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Planet"))
        {
            //Debug.Log(Vector3.Distance(transform.position, g.transform.position));
            if(Vector3.Distance(transform.position, g.transform.position) < distance)
            {
                planetSprites[planetCount].transform.position = transform.position + Vector3.Normalize(g.transform.position - transform.position) * (Vector3.Distance(transform.position, g.transform.position)/distance) * 0.08f;
                planetSprites[planetCount].GetComponent<MeshRenderer>().enabled = true;
                planetCount++;
            }
        }
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Home"))
        {
            //Debug.Log(Vector3.Distance(transform.position, g.transform.position));
            if (Vector3.Distance(transform.position, g.transform.position) < distance)
            {
                planetSprites[planetCount].transform.position = transform.position + Vector3.Normalize(g.transform.position - transform.position) * (Vector3.Distance(transform.position, g.transform.position) / distance) * 0.08f;
                planetSprites[planetCount].GetComponent<MeshRenderer>().enabled = true;
                planetCount++;
            }
        }
        for (int i = planetCount; i < numPlanets; i++)
        {
            planetSprites[i].GetComponent<MeshRenderer>().enabled = false;
        }
	}
}
