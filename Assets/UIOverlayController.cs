using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlayController : MonoBehaviour {
    public List<GameObject> imgs = new List<GameObject>();
    public Sprite sprt;
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
            GameObject t = new GameObject("txt");
            t.transform.SetParent(transform);
            Image myImg = t.AddComponent<Image>();
            myImg.rectTransform.localScale = Vector3.one;
            myImg.rectTransform.localRotation = Quaternion.identity;
            myImg.rectTransform.anchoredPosition3D = Vector3.zero;
            myImg.rectTransform.sizeDelta = new Vector2(50, 50);
            myImg.sprite = sprt;
            myImg.rectTransform.anchorMax = Vector2.zero;
            myImg.rectTransform.anchorMin = Vector2.zero;
            imgs.Add(t);
        }
        int count = 0;
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Planet"))
        {
            imgs[count].GetComponent<Image>().rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(p.transform.position);
            count++;
        }
    }
}
