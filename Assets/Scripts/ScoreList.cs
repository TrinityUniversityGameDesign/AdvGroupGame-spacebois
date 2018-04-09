using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreList : MonoBehaviour {
    public List<GameObject> texts = new List<GameObject>();
    Font ArialFont;
	// Use this for initialization
	void Start () {
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
    }
	
	// Update is called once per frame
	void Update () {
        int count = 0;
        while(texts.Count < PhotonNetwork.playerList.Length)
        {
            GameObject t = new GameObject("txt");
            t.transform.SetParent(transform);
            Text myText = t.AddComponent<Text>();
            myText.font = ArialFont;
            myText.fontSize = 40;
            myText.rectTransform.localScale = Vector3.one;
            myText.rectTransform.localRotation = Quaternion.identity;
            myText.rectTransform.anchoredPosition3D = Vector3.up * 200 - Vector3.up * 50 * texts.Count + Vector3.right * 20;
            myText.rectTransform.sizeDelta = new Vector2(500, 50);
            texts.Add(t);
        }
		foreach(PhotonPlayer p in PhotonNetwork.playerList)
        {
            texts[count].GetComponent<Text>().text = "Player " + p.ID + ": " + p.GetScore(); 
            if(p.ID == PhotonNetwork.player.ID)
            {
                texts[count].GetComponent<Text>().color = Color.green;
            } else
            {
                texts[count].GetComponent<Text>().color = Color.white;
            }
            count += 1;
        }
        for(int i = count; count < texts.Count; i++)
        {
            texts[i].GetComponent<Text>().text = "";
        }
        
	}
}
