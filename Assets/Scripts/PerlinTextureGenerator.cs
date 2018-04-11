using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTextureGenerator : MonoBehaviour {
    Texture2D texture;
    public int resolution = 128;
    public float steps;
    public float percentSea;
    public Color firstCol;
    public Color secondCol;
	// Use this for initialization
	void Start () {
        firstCol = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        secondCol = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        steps = Random.Range(2f, 12f);

        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Perlin Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        
        genTexture();
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Custom/Rim");
        GetComponent<MeshRenderer>().material.SetColor("_RimColor", secondCol);
    }

    public void genTexture()
    {
        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float val = Mathf.PerlinNoise(x * stepSize * steps, y * stepSize * steps);
                Color currCol = firstCol;
                if (val > percentSea)
                    currCol = secondCol;
                texture.SetPixel(x, y, currCol);
            }
        }
        texture.Apply();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
