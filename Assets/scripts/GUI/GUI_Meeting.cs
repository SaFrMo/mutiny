using UnityEngine;
using System.Collections;

public class GUI_Meeting : MonoBehaviour {

	public bool DISPLAYED = false;

	private float liaisonSpeechWidth;
	private float liaisonSpeechHeight;

	Rect liaisonSpeechLocation;
	Rect itemMenuLocation;
	Rect infoMenuLocation;

	void Start () {
		liaisonSpeechWidth = Screen.width * .5f;
		liaisonSpeechHeight = Screen.height * .25f;

		liaisonSpeechLocation = new Rect (0, Screen.height - liaisonSpeechHeight, liaisonSpeechWidth, liaisonSpeechHeight);
		itemMenuLocation = new Rect (Screen.width * .5f, Screen.height * .25f, Screen.width * .25f, Screen.height * .75f);
		infoMenuLocation = new Rect (Screen.width * .75f, Screen.height * .25f, Screen.width * .25f, Screen.height * .75f);
	}

	void DrawLiaisonSpeech () {
		string liaisonSpeech = string.Empty;
		foreach (string s in Report.REPORT_TEXT) {
			liaisonSpeech += s;
		}
		GUI.BeginGroup (liaisonSpeechLocation);
		GUI.Box (new Rect (0, 0, liaisonSpeechWidth, liaisonSpeechHeight), liaisonSpeech);
		GUI.EndGroup();
	}



	void OnGUI () {
		//if (DISPLAYED) {
			DrawLiaisonSpeech();
		//}
	}
}
