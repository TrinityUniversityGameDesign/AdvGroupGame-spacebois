using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayController : MonoBehaviour {
    public List<GameObject> imgs = new List<GameObject>();
    public Sprite sprt;
    public Sprite enem;
    public List<GameObject> texts = new List<GameObject>();
    Font ArialFont;
    // Use this for initialization
    void Start()
    {
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
    }
	
	// Update is called once per frame
	void Update () {
        while (imgs.Count < GameObject.FindGameObjectsWithTag("Collect").Length + GameObject.FindGameObjectsWithTag("Enemy").Length)
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
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Collect"))
        {
            if(Vector3.Distance(Camera.main.transform.position, p.transform.position) < 5000)
            {
                Vector3 relativePosition = Camera.main.transform.InverseTransformPoint(p.transform.position);
                relativePosition = new Vector3(relativePosition.x, relativePosition.y, Mathf.Max(relativePosition.z, 1.0f));
                relativePosition = Camera.main.transform.TransformPoint(relativePosition);

                imgs[count].GetComponent<Image>().sprite = sprt;
                texts[count].GetComponent<Text>().color = Color.green;

                imgs[count].GetComponent<Image>().rectTransform.anchoredPosition = (Vector2)Camera.main.WorldToViewportPoint(relativePosition) - GetComponent<RectTransform>().sizeDelta / 2f;
                texts[count].GetComponent<Text>().rectTransform.anchoredPosition = (Vector2)Camera.main.WorldToViewportPoint(relativePosition) - GetComponent<RectTransform>().sizeDelta / 2f + new Vector2(0,30);
                texts[count].GetComponent<Text>().text = "" + (int)Vector3.Distance(Camera.main.transform.position, p.transform.position);//p.GetComponent<planetInfo>().name;//
                Vector2 size = new Vector2(30, 30); // * (1f - Vector3.Distance(Camera.main.transform.position, p.transform.position) / 5000f);
                imgs[count].GetComponent<Image>().rectTransform.sizeDelta = size;
                count++;
            }
        }
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (Vector3.Distance(Camera.main.transform.position, p.transform.position) < 5000)
            {
                Vector3 relativePosition = Camera.main.transform.InverseTransformPoint(p.transform.position);
                relativePosition = new Vector3(relativePosition.x, relativePosition.y, Mathf.Max(relativePosition.z, 1.0f));
                relativePosition = Camera.main.transform.TransformPoint(relativePosition);

                imgs[count].GetComponent<Image>().sprite = enem;
                texts[count].GetComponent<Text>().color = Color.red;

                imgs[count].GetComponent<Image>().rectTransform.anchoredPosition = (Vector2)Camera.main.WorldToViewportPoint(relativePosition) - GetComponent<RectTransform>().sizeDelta / 2f;
                texts[count].GetComponent<Text>().rectTransform.anchoredPosition = (Vector2)Camera.main.WorldToViewportPoint(relativePosition) - GetComponent<RectTransform>().sizeDelta / 2f + new Vector2(0, 30);
                texts[count].GetComponent<Text>().text = "" + (int)Vector3.Distance(Camera.main.transform.position, p.transform.position);//p.GetComponent<planetInfo>().name;//
                Vector2 size = new Vector2(30, 30); // * (1f - Vector3.Distance(Camera.main.transform.position, p.transform.position) / 5000f);
                imgs[count].GetComponent<Image>().rectTransform.sizeDelta = size;
                count++;
            }
        }
    }
}
