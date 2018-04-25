using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle: MonoBehaviour
{

	// Use this for initialization

	[SerializeField]
	private Light sun;                         //directional light souce built into unity
	[SerializeField]
	private float secondsInDay = 120f;         //length of full day/night cycle

	[Range(0, 1)]
	[SerializeField]
	private float currTimeOfDay;               //time of day (position of sun represented as a range between 0 and 1)
	private float timeMultiplier = 1f;         //how fast the sun moves through the scene
	private float sunInitialIntensity;         //to create intensity effect on sunrise/sunset

	void Start()
	{
		sunInitialIntensity = sun.intensity;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateSun();  //function below that moves sun across scene and adjusts intensity of sun
		currTimeOfDay += (Time.deltaTime / secondsInDay) * timeMultiplier; //move sun within game time
		if (currTimeOfDay >= 1) //reset day when timeofday is out of range
		{
			currTimeOfDay = 0; //reset full cycle
		}
	}

	void UpdateSun() //function that moves sun across scene and adjusts intensity of sun
	{
		sun.transform.localRotation = Quaternion.Euler((currTimeOfDay * 360f) - 90, 170, 0); //rotation of the sun on x, y, z axis

		float intensityMultiplier = 1f; //regulates intensity of the sun

		if (currTimeOfDay <= 0.25f || currTimeOfDay >= 0.75f) //!sunrise || sunset -- "daytime" -- no change to sun intensity
		{
			intensityMultiplier = 0;
		}
		else if (currTimeOfDay <= 0.25f) //Sunrise!
		{
			intensityMultiplier = Mathf.Clamp01((currTimeOfDay - 0.25f) * (1 / 0.02f)); //set intensity multiplier when sunrises to allow it to fade in
		}
		else if (currTimeOfDay >= 0.75f) //Sunset!
		{
			intensityMultiplier = Mathf.Clamp01(1 - ((currTimeOfDay - 0.075f) * (1 / 0.02f))); //fade sun intensity out on sunset;
		}
		sun.intensity = sunInitialIntensity * intensityMultiplier;

	}
}
