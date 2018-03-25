using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinStripeGenerator : MonoBehaviour {
    Texture2D texture;
    public int resolution = 128;
    public float steps;
    public Color initCol;
    // Use this for initialization
    void Start()
    {
        initCol = Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);
        steps = Random.Range(2f, 12f);
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Perlin Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        genTexture();
        GetComponent<MeshRenderer>().material.shader = Shader.Find("Custom/Rim");
        GetComponent<MeshRenderer>().material.SetColor("_RimColor", initCol);
        transform.rotation = Random.rotation;
    }

    public void genTexture()
    {
        float stepSize = 1f / resolution;
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float val = Mathf.PerlinNoise(0, y * stepSize * steps);
                texture.SetPixel(x, y, (val*2f) * initCol);
            }
        }
        texture.Apply();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
