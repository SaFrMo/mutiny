using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Meeting : IGUIMenu {

	// interface members
	public override string ButtonName() {
		return "Meeting with Liaison";
	}

	public override void Display () {
		DISPLAYED = true;
	}

	public override void Hide() {
		DISPLAYED = false;
	}

	// class members	
	public GUISkin skin;
	
	public bool DISPLAYED = true;

	public static string CONTEXTUAL_STIMULUS = "This is a test.";
	public static Dictionary<string, int> CONTEXTUAL_PLAYER_LINES = new Dictionary<string, int>();
	public static int CONTEXTUAL_INDEX;

	private float liaisonSpeechWidth;
	private float liaisonSpeechHeight;
	
	private float quarterScreenWidth;
	private float quarterScreenHeight;
	
	private float spacer = 0;

	//private string liaisonSpeechToDisplay;
	private static List<string> liaisonSpeechList = new List<string>();
	public static void SET_LIAISON_SPEECH (string text, bool clearList = true) {
		if (clearList)
			liaisonSpeechList.Clear ();
		liaisonSpeechList.Add (text);
	}
	public static void SET_LIAISON_SPEECH (List<string> textList, bool clearList = true) {
		if (clearList)
			liaisonSpeechList.Clear();
		liaisonSpeechList = textList;
	}
	
	void Start () {
		quarterScreenWidth = Screen.width * .25f;
		quarterScreenHeight = Screen.height * .25f;
	}

	void DrawLiaisonSpeech () {
		string liaisonSpeechToDisplay = string.Empty;
		foreach (string s in liaisonSpeechList) {//Report.REPORT_TEXT) {
			liaisonSpeechToDisplay += s + "\n";
		}
		GUI.BeginGroup (new Rect (0, quarterScreenHeight * 3, quarterScreenWidth * 2, quarterScreenHeight));
		GUI.Box (new Rect (0, 0, quarterScreenWidth * 2, quarterScreenHeight), liaisonSpeechToDisplay);
		GUI.EndGroup();
	}
	
	
	
	// MEETING STEP 2. ORDERS
	// =========================
	// Give Liaison his/her instructions. Bring X thing, smuggle Y thing in, talk to X person.
	Orders ordersLocation = Orders.MainMenu;
	int numberOfTasks = 0;
	int maxTasks = 3;
	string errorMessage = string.Empty;
	
	enum Reports {
		
	};
	
	enum Orders {
		MainMenu = 0,
		TalkTo = 1,
		ErrorMessage = 2,
		ReportOn = 3,
		BringMe = 4,
		ContextualQuestions
	};
	
	void MainMenuButton () {
		if (GUILayout.Button ("[Go back]")) {
			ordersLocation = 0;
		}
	}
	
	void DrawDialogue () {
		GUILayout.BeginArea (new Rect (quarterScreenWidth * 2, quarterScreenHeight * 2, quarterScreenWidth, quarterScreenHeight * 2));
		
		
		switch (ordersLocation) {
			
			// main concourse of orders
		case Orders.MainMenu:
			// reports subheading
			GUILayout.Box ("Reports");
			if (GUILayout.Button ("Report on...")) {
				ordersLocation = Orders.ReportOn;
			}
			// orders subheading
			GUILayout.Box ("Orders");
			if (GUILayout.Button ("Talk to...")) {
				ordersLocation = Orders.TalkTo;
			}
			if (GUILayout.Button ("Bring me...")) {
				ordersLocation = Orders.BringMe;
			}
			if (GUILayout.Button ("[Contextual questions...]")) {
				ordersLocation = Orders.ContextualQuestions;
			}
			break;

			
		case Orders.TalkTo:
			foreach (GameObject go in GAME_MANAGER.Roster) {
				if (GUILayout.Button (go.name + ".")) {
					if (numberOfTasks < maxTasks) {
						if (ToDoList.TODO_LIST.Contains (string.Format ("Talk to {0}.", go.name))) {
							SET_LIAISON_SPEECH ( string.Format ("I'm already going to talk to {0}.", go.name));
						}
						else {
							ToDoList.TODO_LIST.Add (string.Format ("Talk to {0}.", go.name));

							SET_LIAISON_SPEECH (GAME_MANAGER.GET_RESPONSE (go,
							                                             string.Format ("I don't see what good that'll do, but I'll talk to {0}.", go.name),
							                                             string.Format ("There's not going to be much changing their mind, but alright."),
							                                             string.Format ("Alright, I'll talk to {0}.", go.name),
							                                             string.Format ("I think {0} is leaning toward us. That's a good idea.", go.name),
							                                             string.Format ("I'll check in with our friend {0}.", go.name),
							                                             string.Format ("{0} is very much on board already, but I'll stop by anyway.", go.name)));




							ordersLocation = Orders.MainMenu;
							numberOfTasks++;
						}
					}
					else {
						if (ToDoList.TODO_LIST.Contains (string.Format ("Talk to {0}.", go.name))) {
							SET_LIAISON_SPEECH ( string.Format ("I'm already going to talk to {0}.", go.name));
						}
						else {
							SET_LIAISON_SPEECH ("I won't have time for that many errands.");
						}
					}
				}
			}
			MainMenuButton();
			break;
			
		case Orders.ReportOn:
			MainMenuButton();
			break;
			
		case Orders.BringMe:
			MainMenuButton();
			break;

		case Orders.ContextualQuestions:
			SET_LIAISON_SPEECH (CONTEXTUAL_STIMULUS);
			foreach (KeyValuePair<string, int> kv in CONTEXTUAL_PLAYER_LINES) {
				if (GUILayout.Button (kv.Key)) {
					CONTEXTUAL_INDEX = kv.Value;
				}
			}
			MainMenuButton();
			break;

			
		case Orders.ErrorMessage:
			GUILayout.Box (errorMessage);
			if (GUILayout.Button ("[Back to roster]")) {
				ordersLocation = Orders.TalkTo;
			}
			MainMenuButton();
			break;
		};
		
		
		
		
		GUILayout.EndArea();
	}
	
	void DrawLiaisonToDoList () {
		GUILayout.BeginArea (new Rect (quarterScreenWidth * 3, quarterScreenHeight * 2, quarterScreenWidth, quarterScreenHeight * 2));
		GUILayout.Box ("LIAISON'S TO-DO LIST.");
		string[] tasks = ToDoList.TODO_LIST.ToArray();
		// TODO: Add tooltip
		foreach (string s in tasks) {
			if (GUILayout.Button (s)) {
				ToDoList.TODO_LIST.Remove (s);
				numberOfTasks--;
				SET_LIAISON_SPEECH (string.Format ("Okay, I won't {0}{1}", s[0].ToString().ToLower(), s.Substring(1)));
			}
		}
		GUILayout.EndArea();
	}
	
	
	
	void OnGUI () {
		GUI.skin = skin;
		if (DISPLAYED) {
			DrawLiaisonSpeech();
			DrawDialogue();
			DrawLiaisonToDoList();
		}
	}
}