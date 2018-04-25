using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpeedView : MonoBehaviour
{
    Font ArialFont;
    Text myText;
    private float speed;
    private float engineCap;
    //private Image energyImage; // fillAmount not working
    private float energyAmount;
    private bool isFull = false;
    private float energyAmountMax;

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
        myText.alignment = TextAnchor.LowerCenter;

        energyAmountMax = this.transform.parent.parent.parent.GetComponent<PlayerMovement>().speedExhaustScale;
        //energyImage = this.transform.Find("ProgressIndicator").GetComponent<Image>();
        //energyImage.fillMethod = Image.FillMethod.Radial360;

    }

    // Update is called once per frame
    void Update()
    {
        speed = this.transform.parent.parent.parent.GetComponent<PlayerMovement>().speed;
        energyAmount = this.transform.parent.parent.parent.GetComponent<PlayerMovement>().speedExhaust;
        engineCap = (energyAmount / energyAmountMax) * 100;

        myText.rectTransform.localScale = Vector3.one;
        myText.rectTransform.localRotation = Quaternion.identity;
        myText.color = Color.yellow;
        myText.text = "Speed : " + speed + "\nEngine : " + Mathf.RoundToInt(engineCap) + "%";
        myText.rectTransform.sizeDelta = new Vector2(500, 200);

        //energyImage.fillAmount = Random.value;
        //Debug.Log("fill amount: " + energyImage.fillAmount);

    }
}
