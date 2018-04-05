using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayController : MonoBehaviour {
    public List<GameObject> imgs = new List<GameObject>();
    public Sprite sprt;
    public List<GameObject> texts = new List<GameObject>();
    Font ArialFont;
    // Use this for initialization
    void Start()
    {
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
    }
	
	// Update is called once per frame
	void Update () {
        while (imgs.Count < GameObject.FindGameObjectsWithTag("Planet").Length)
        {
            GameObject t = new GameObject("img");
            t.transform.SetParent(transform);
            Image myImg = t.AddComponent<Image>();
            myImg.rectTransform.localScale = Vector3.one;
            myImg.rectTransform.localRotation = Quaternion.identity;
            myImg.rectTransform.anchoredPosition3D = Vector3.zero;
            myImg.rectTransform.sizeDelta = new Vector2(30, 30);
            myImg.sprite = sprt;
            myImg.rectTransform.anchorMax = Vector2.zero;
            myImg.rectTransform.anchorMin = Vector2.zero;
            imgs.Add(t);

            t = new GameObject("txt");
            t.transform.SetParent(transform);
            Text myText = t.AddComponent<Text>();
            myText.font = ArialFont;
            myText.fontSize = 20;
            myText.color = Color.green;
            myText.rectTransform.localScale = Vector3.one;
            myText.rectTransform.localRotation = Quaternion.identity;
            myText.rectTransform.anchoredPosition3D = Vector3.zero;
            myText.rectTransform.sizeDelta = new Vector2(200, 50);
            myText.rectTransform.anchorMax = Vector2.zero;
            myText.rectTransform.anchorMin = Vector2.zero;
            myText.alignment = TextAnchor.MiddleCenter;
            texts.Add(t);
        }
        int count = 0;
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Planet"))
        {
            if(Vector3.Distance(Camera.main.transform.position, p.transform.position) < 5000)
            {
                Vector3 relativePosition = Camera.main.transform.InverseTransformPoint(p.transform.position);
                relativePosition = new Vector3(relativePosition.x, relativePosition.y, Mathf.Max(relativePosition.z, 1.0f));
                relativePosition = Camera.main.transform.TransformPoint(relativePosition);

                imgs[count].GetComponent<Image>().rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(relativePosition);
                texts[count].GetComponent<Text>().rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(relativePosition) + new Vector3(0,20);
                texts[count].GetComponent<Text>().text = p.GetComponent<planetInfo>().name;//"" + Vector3.Distance(Camera.main.transform.position, p.transform.position);
                Vector2 size = new Vector2(30, 30); // * (1f - Vector3.Distance(Camera.main.transform.position, p.transform.position) / 5000f);
                imgs[count].GetComponent<Image>().rectTransform.sizeDelta = size;
                count++;
            }
             
        }
    }
}
