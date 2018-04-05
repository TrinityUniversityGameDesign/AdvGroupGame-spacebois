using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planetInfo : MonoBehaviour {
    public string name;
    string consonants = "WRTPSDFGHJKLZXCVBNM";
    string vowels = "AEIOUY";
	// Use this for initialization
	void Start () {
		for(int i = 0; i < Random.Range(3,10); i++)
        {
            if (i % 2 == 0)
                name += consonants.ToCharArray()[Random.Range(0, consonants.Length)];
            else
                name += vowels.ToCharArray()[Random.Range(0, vowels.Length)];
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
