using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_StatsDisplay : MonoBehaviour, IGUIMenu {

	// interface members
	public string ButtonName() {
		return "Personal View";
	}

	public void Display () {
		DISPLAYED = true;
	}
	
	public void Hide() {
		DISPLAYED = false;
	}

	// class
	public bool DISPLAYED = false;
	
	// PLACEHOLDERS FOR IN-GAME ITEMS
	string meager = "Meager Meal";
	float meagerAmount = 5f;
	float glutton = 80f;
	float gluttonPenalty = 5f;

	/*
	float AwakeBoost (float timeInMinutes) {
		// S-shaped curve that peaks around 7 hours and stays up after that
	}
	*/
	
	float exerciseBoostPerHour = 5f;
	float exerciseHungerCostPerHour = 10f;
	float exerciseAwakeCostPerHour = 5f;
	// END THE AFOREMENTIONED
	
	public GUISkin skin;
	Dictionary<string, bool> actions = new Dictionary<string, bool>() {
		{ "Full Stomach", false },
		{ "Awake", false },
		{ "Fit", false },
		{ "Mindful", false },
		{ "Convicted", false },
		{ "Well-Disposed", false }
	};
	
	List<string> inventory = new List<string>();

	
	
	// ACTION DISPLAY
	// ================
	// Much of the in-game action will take place here - maybe make its own
	// class and static function to simplify this class?
	string actionsToShow = string.Empty;
	
	void Exercise (float timeInMinutes) {
		//float fractionOfAnHour = timeInMinutes / 60f;
		if (!timeAllotment.ContainsKey ("Exercise")) {
			timeAllotment.Add ("Exercise", (timeInMinutes / minutesConstant) * (Screen.width / maxTime));
		}
		else {
			timeAllotment["Exercise"] = (timeInMinutes / minutesConstant) * (Screen.width / maxTime);
		}
		//Player.ChangeStat("FIT", exerciseBoostPerHour * fractionOfAnHour);
		//Player.ChangeStat ("FULL_STOMACH", -exerciseHungerCostPerHour * fractionOfAnHour);
		//Player.AWAKE -= exerciseAwakeCostPerHour * fractionOfAnHour;
	}
	
	void ActionsMenu () {
		switch (actionsToShow) {
			
		case "Full Stomach":
			//if (inventory.Contains (meager)) {
				if (GUILayout.Button ("Eat Meager Meal")) {
					Player.ChangeStat("FULL_STOMACH", meagerAmount);
					if (Player.FULL_STOMACH > glutton) {
						Player.ChangeStat("FIT", -gluttonPenalty);
					}
					inventory.Remove (meager);
					// TODO: some kind of animation
				}   
			//}
			break;
			
		case "Awake":
			// slider to adjust how long to sleep (reflect in "Time" bar)
			break;
			
		case "Fit":
			GUILayout.Label ("Exercise for...");
			if (GUILayout.Button ("15 minutes")) {
				Exercise (15f);
			}
			else if (GUILayout.Button ("30 minutes")) {
				Exercise (30f);
			}
			break;
			
		case "Mindful":
			break;
			
		case "Convicted":
			break;
			
		case "Well-Disposed":
			break;
		};
	}
	
	
	// GRAPH DISPLAY
	// ================
	
	public float spacer = 10f;
	public float maxGraphWidth;
	//public float timeAllotted = 0;
	private float taskTime = 50f;
	private float timePassed;

	// number of seconds to allow per turn - v. important
	// IDEA: 144 seconds in a day (1 second realtime = 10 minutes game time)
	public float maxTime = 144f;
	// how many minutes per maxTime unit
	private float minutesConstant;
	private float pixelsPerSecond;


	
	public Texture2D fullStomachGraph;
	public Texture2D awakeGraph;
	public Texture2D fitGraph;
	public Texture2D mindfulnessGraph;
	public Texture2D convictionGraph;
	public Texture2D dispositionGraph;
	public Texture2D blackGraph;
	public Texture2D timeSpent;
	public Texture2D timeAllottedGraph;


	// Time allotment
	Dictionary<string, float> timeAllotment = new Dictionary<string, float>();
	
	
	void DisplayStat (string name, float toDisplay, Texture2D graphBar, float scale = 100f, bool includeSpace = true) {
		
		GUILayout.BeginHorizontal();
		// activates options relating to that stat
		if (GUILayout.Button (name, GUILayout.Width(100f))) {
			actionsToShow = name;
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

	// Color.grey is just a placeholder
	Color originalGUIColor = Color.grey;

	void OnGUI () {
		if (DISPLAYED) {
			if (originalGUIColor == Color.grey) {
				originalGUIColor = GUI.color;
			}
			// minutesConstant = game-time minutes per real-time second
			// 1440 = 24 hours * 60 minutes
			minutesConstant = 1440f / maxTime;
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
			
			// Left side
			GUILayout.BeginArea (new Rect (spacer, spacer, Screen.width / 2 - spacer * 3, Screen.height - spacer * 2));
			ActionsMenu ();
			GUILayout.EndArea();
			
			// Time
			float timeHeight = 80f;
			timeAllottedGraph.wrapMode = TextureWrapMode.Repeat;

			// 1 second realtime = x pixels = x minutes game time
			pixelsPerSecond = Screen.width / maxTime;
			// this is the black background for the time graph
			GUI.DrawTexture (new Rect(0, Screen.height - timeHeight, Screen.width, timeHeight), blackGraph);
			// this is time that has already passed
			timePassed = Screen.width * (Time.time / maxTime); 
			GUI.DrawTexture (new Rect(0, Screen.height - timeHeight, timePassed, timeHeight), timeSpent);
			// this is time that is allotted
			GUI.BeginGroup (new Rect(timePassed, Screen.height - timeHeight, Screen.width, timeHeight));
			float currentActivityX = timePassed;
			foreach (KeyValuePair<string, float> kv in timeAllotment) {
				GUI.color = ((int)Time.time % 2 == 0 ? new Color (GUI.color.r, GUI.color.g, GUI.color.b, 0.5f) : originalGUIColor);
				GUI.DrawTexture (new Rect(0, 0, kv.Value, timeHeight), timeAllottedGraph);
				GUI.color = originalGUIColor;
				currentActivityX += kv.Value;
			}
			GUI.EndGroup();
		}
	}
}