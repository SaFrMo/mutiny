using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewGame : MonoBehaviour {

	public GUISkin skin;

	private float letterboxHeight;
	private float subtitlesWidth;
	private float subtitlesHeight;

	private int subtitlesIndex = 0;

	private Texture2D black;

	private List<string> subtitleList = new List<string>() {
		"Tom Walker reported for duty every morning and ate and drank with his friends every night.",
		"Once he won a large pot in a game of cards, and once he took ill for three days and turned sickly in the skin, and these are the only remarkable stories from the first part of his life.",
		"One day, though, while he was securing cannon, he shirked his duty and took a shortcut the other boys had taught him.",
		"It would be fine, since the ship didn’t look bound for bad weather at all, and he knew he deserved a rest after working so hard that same morning.",
		"But the ship sailed into the heart of a terrible storm...",
		"...and a cannon broke free of its bonds.",
		"Take a marble and set it on a dinner plate. Dip and turn and twist the plate around, and try to keep control of the marble.",
		"Now, picture the marble as a multi-ton cannon, and the plate surface as a structure of wood and rope, pitching and yawing, peopled with hardware, explosives, and your coworkers, bunkmates, and friends.",
		"Tom Walker had become a loose cannon - a danger to us all. He and everyone else knew it was his fault, and so he jumped in himself to snare the beast.",
		"After a terrible wrangling with the swaying behemoth, diving all aboard the black, rain-soaked deck, he managed to detain the cannon and blocked up its wheels.",
		"He had taken responsibility for a foolish mistake and put himself at great personal risk to do so, and in the eyes of the crew, all was forgiven. And so we cheered when the captain told us to pin the Cross of Honor on him.",
		"And so we froze when the captain told us to shoot him.",
		"\"Carelessness has compromised this vessel. At this very hour it is perhaps lost. To be at sea is to be in front of the enemy. A ship making a voyage is an army waging war. The tempest is concealed, but it is at hand. The whole sea is an ambuscade.\"",
		"\"Death is the penalty of any misdemeanor committed in the face of the enemy. No fault is reparable. Courage should be rewarded, and negligence punished.\"",
		"\"Let it be done.\"",
		string.Empty,
		null
	};

	public string CurrentSubtitle {
		get {
			return subtitleList[subtitlesIndex];
		}
	}

	private void Letterboxing () {
		// top matte
		GUI.DrawTexture (new Rect (0, 0, Screen.width, letterboxHeight), black);
		// bottom matte
		GUI.DrawTexture (new Rect (0, Screen.height - letterboxHeight, Screen.width, letterboxHeight), black);
	}

	private bool shrink = false;
	private IEnumerator FadeOutLetterbox () {
		letterboxHeight--;
		yield return null;
	}

	private void Subtitles () {
		GUI.Box (new Rect (Screen.width / 2 - subtitlesWidth / 2, Screen.height - subtitlesHeight, subtitlesWidth, subtitlesHeight), CurrentSubtitle,
		         skin.customStyles[0]);
		string ellipses = ((int)Time.time % 2 == 0 ? "..." : null);
		if (CurrentSubtitle != string.Empty) {
			if (GUI.Button (new Rect (Screen.width - 100f, Screen.height - 100f, 100f, 100f), ellipses)) {
				subtitlesIndex++;
			}
		}
		else {
			if (!shrink)
				shrink = true;
		}
	}

	void Start () {
		letterboxHeight = Screen.height - (Screen.width / 2.39f);	
		black = SaFrMo.CreateColor (Color.black);
		subtitlesWidth = Screen.width * .75f;
		subtitlesHeight = letterboxHeight * .75f;
	}

	void Update () {
		if (shrink && letterboxHeight >= 0)
			StartCoroutine (FadeOutLetterbox());
	}


	void OnGUI () {
		GUI.skin = skin;
		Letterboxing();
		Subtitles();
	}
}
