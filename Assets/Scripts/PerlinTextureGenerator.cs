using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTextureGenerator : MonoBehaviour {
    Texture2D texture;
    public int resolution = 128;
    public float steps;
    public float percentSea;
	// Use this for initialization
	void Start () {
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Perlin Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        genTexture();
    }

    public void genTexture()
    {
        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float val = Mathf.PerlinNoise(x * stepSize * steps, y * stepSize * steps);
                Color currCol = Color.blue;
                if (val > percentSea)
                    currCol = Color.green;
                texture.SetPixel(x, y, currCol);
            }
        }
        texture.Apply();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
