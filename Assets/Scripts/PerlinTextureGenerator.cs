using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTextureGenerator : MonoBehaviour {
    Texture2D texture;
    public int resolution = 128;
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
                texture.SetPixel(x, y, new Color(x * stepSize, y * stepSize, 0f));
            }
        }
        texture.Apply();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
