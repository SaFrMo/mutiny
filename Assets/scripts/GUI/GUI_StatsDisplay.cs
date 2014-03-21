using UnityEngine;
using System.Collections;

public class GUI_StatsDisplay : MonoBehaviour {

	public GUISkin skin;

	public float spacer = 10f;
	public float maxGraphWidth;

	public Texture2D fullStomachGraph;
	public Texture2D awakeGraph;
	public Texture2D fitGraph;
	public Texture2D mindfulnessGraph;
	public Texture2D convictionGraph;
	public Texture2D dispositionGraph;
	public Texture2D blackGraph;


	void ActivateStatInfo (string whichStat) {}

	void DisplayStat (string name, float toDisplay, Texture2D graphBar, float scale = 100f, bool includeSpace = true) {

		GUILayout.BeginHorizontal();
		// activates options relating to that stat
		if (GUILayout.Button (name, GUILayout.Width(100f))) {
			ActivateStatInfo (name);
		}
		// creates the appropriate box size
		GUILayout.Box (string.Empty);
		// black background
		GUI.DrawTexture (GUILayoutUtility.GetLastRect(), blackGraph);
		// representative graph
		GUI.DrawTexture (new Rect (GUILayoutUtility.GetLastRect().x,
		                           GUILayoutUtility.GetLastRect().y,
		                           (toDisplay / scale) * GUILayoutUtility.GetLastRect().width,
		                           GUILayoutUtility.GetLastRect().height), graphBar);
		GUILayout.EndHorizontal();
		if (includeSpace) {
			GUILayout.Label (string.Empty);
		}

	}

	void OnGUI () {
		GUI.skin = skin;
		maxGraphWidth = 100f;

		// Right side

		GUILayout.BeginArea (new Rect (Screen.width / 2 - spacer, spacer, (Screen.width / 2) - spacer, Screen.height - (spacer * 2)));
		GUILayout.Label ("Personal Statistics");
		GUILayout.Label (string.Empty);

		DisplayStat ("Full Stomach", Player.FULL_STOMACH, fullStomachGraph);
		DisplayStat ("Awake", Player.AWAKE, awakeGraph);
		DisplayStat ("Fit", Player.FIT, fitGraph);
		// display inventory
		DisplayStat ("Mindful", Player.MINDFULNESS, mindfulnessGraph);
		DisplayStat ("Convicted", Player.CONVICTION, convictionGraph);
		DisplayStat ("Well-Disposed", Player.DISPOSITION, dispositionGraph);

		GUILayout.EndArea ();



	}
}
