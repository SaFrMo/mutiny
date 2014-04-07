using UnityEngine;
using System.Collections;

public class NewGame : MonoBehaviour {

	public GUISkin skin;

	private float letterboxHeight;
	private float subtitlesWidth;
	private float subtitlesHeight;

	private Texture2D black;

	private string currentSubtitle = "Tom Walker reported for duty every morning and ate and drank with his friends every night.";
	public string CurrentSubtitle {
		get {
			return currentSubtitle;
		}
		set {
			currentSubtitle = value;
		}
	}

	private void Letterboxing () {
		// top matte
		GUI.DrawTexture (new Rect (0, 0, Screen.width, letterboxHeight), black);
		// bottom matte
		GUI.DrawTexture (new Rect (0, Screen.height - letterboxHeight, Screen.width, letterboxHeight), black);
	}

	private void Subtitles () {
		GUI.Box (new Rect (Screen.width / 2 - subtitlesWidth / 2, Screen.height - subtitlesHeight, subtitlesWidth, subtitlesHeight), currentSubtitle,
		         skin.customStyles[0]);
	}

	void Start () {
		letterboxHeight = Screen.height - (Screen.width / 2.39f);	
		black = SaFrMo.CreateColor (Color.black);
		subtitlesWidth = Screen.width * .75f;
		subtitlesHeight = letterboxHeight * .75f;
	}


	void OnGUI () {
		GUI.skin = skin;
		Letterboxing();
		Subtitles();
	}
}
