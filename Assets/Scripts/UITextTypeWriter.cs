using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour 
{

	Text txt;
	string story;

	void Awake () 
	{
		txt = GetComponent<Text> ();
		story = txt.text;
		txt.text = "";

		// TODO: add optional delay when to start
		StartCoroutine ("PlayText");
	}

	IEnumerator PlayText()
	{
		foreach (char c in story) 
		{
			txt.text += c;
			yield return new WaitForSeconds (0.04f);
		}

		yield return new WaitForSeconds (2f);

		txt.text = "";
		story = "Look around to see your surroundings and how to control your craft...";

			foreach (char c in story) 
			{
				txt.text += c;
				yield return new WaitForSeconds (0.04f);
			}

		yield return new WaitForSeconds (2f);

		txt.text = "";
		story = "When you are ready, hold your gaze at the launch button. Good luck out there...";

			foreach (char c in story) 
			{
				txt.text += c;
				yield return new WaitForSeconds (0.04f);
			}

	}

}
