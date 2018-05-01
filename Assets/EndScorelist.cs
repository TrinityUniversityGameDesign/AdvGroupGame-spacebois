using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScorelist : MonoBehaviour {
    public List<GameObject> texts = new List<GameObject>();
    Font ArialFont;
    // Use this for initialization
    void Start()
    {
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        while (texts.Count < 10)
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
        foreach (PhotonPlayer p in ScoreContainer.scores)
        {
            texts[count].GetComponent<Text>().text = "Player " + p.ID + ": " + p.GetScore();
            texts[count].GetComponent<Text>().color = Color.white;

            count += 1;
        }
        texts[8].GetComponent<Text>().text = "Press any Button";
        texts[9].GetComponent<Text>().text = "To restart";
        for (int i = count; i < 8; i++)
        {
            //Debug.Log(i + ", " + texts.Count);
            texts[i].GetComponent<Text>().text = "";
        }
        

        if(Input.touchCount == 1 || Input.GetKeyDown(KeyCode.S)){
                SceneManager.LoadScene ("Main Menu");
        }

    }
}
