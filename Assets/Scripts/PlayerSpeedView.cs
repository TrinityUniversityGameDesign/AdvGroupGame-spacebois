using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedView : MonoBehaviour
{
    Font ArialFont;
    Text myText;
    private float speed;
    private Image energyImage;
    private float energyAmount;
    private bool isFull = false;

    // Use this for initialization
    void Start()
    {
        ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        GameObject t = new GameObject("txt");
        t.transform.SetParent(transform);
        myText = t.AddComponent<Text>();
        myText.rectTransform.anchoredPosition3D = Vector3.up * 250;
        myText.font = ArialFont;
        myText.fontSize = 40;
        myText.alignment = TextAnchor.MiddleRight;

        energyImage = this.transform.Find("ProgressIndicator").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        speed = this.transform.parent.parent.parent.GetComponent<PlayerMovement>().speed;
        myText.rectTransform.localScale = Vector3.one;
        myText.rectTransform.localRotation = Quaternion.identity;
        myText.color = Color.yellow;
        myText.text = "Speed : " + speed;
        myText.rectTransform.sizeDelta = new Vector2(500, 50);

        energyImage.fillAmount = .5f;
        energyImage.color = Color.green;
    }
}
