using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderChange : MonoBehaviour {

    public Renderer rend;
    public Color idleColor = new Color(0.048F, 0.682F, 0.392F);

	// Use this for initialization
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Custom/FakeVolumetricLightShader");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rend.material.SetColor("_MyColor", idleColor);
        }
    }
}
