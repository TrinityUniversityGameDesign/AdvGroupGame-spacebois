using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMeshModifier : MonoBehaviour {
    Mesh mesh;
    Vector3[] verts;
    // Use this for initialization
    void Start () {
        mesh = GetComponent<MeshFilter>().mesh;
        verts = mesh.vertices;
        Vector3[] newVerts = new Vector3[verts.Length];
        int i = 0;
        foreach(Vector3 v in verts)
        {
            Vector3 vertPos = transform.TransformPoint(v);
            //Debug.Log(vertPos);
            newVerts[i] = vertPos + Vector3.one * Mathf.PerlinNoise(vertPos.x, vertPos.y);
            i++;
        }
        mesh.vertices = newVerts;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        gameObject.AddComponent<SphereCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
