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

	public override string ToolTip () {
		string message = "Request reports from, give orders to, obtain equipment from liaison.";
		return message;
	}

	public new List<Ingredient> Requirements = null;

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

	// Liaison Speech: The liaison will format the members of this list into a report.
	// The default value of liaisonSpeechList is the first thing the liaison will say.
	private static List<string> liaisonSpeechList = new List<string>() {
		"I'm thinking you'll need some writing supplies, so I brought you a quill and a leaf of paper - I can't take too much at one time or they'll notice me. " +
		"You'll probably need some more materials, though - what can I bring you? I can also relay information to you about " +
		"what's going on throughout the rest of the ship. " +
		"\n\nIn any case, you're my charge. Just say the word and it'll be done."
	};
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
		// meeting window is the first thing available
		base.ShowInNav = true;
		quarterScreenWidth = Screen.width * .25f;
		quarterScreenHeight = Screen.height * .25f;
	}

	// what happened since the last turn?
	private static List<string> sinceLastTurn = new List<string>();

	void DrawEffectsSinceLastTurn () {
		GUILayout.BeginArea (new Rect (0, quarterScreenHeight * 2, quarterScreenWidth * 2, quarterScreenHeight));
		foreach (string s in sinceLastTurn) {
			GUILayout.Box (s);
		}
		GUILayout.EndArea();
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
	public static int numberOfTasks = 0;
	private int maxTasks = 3;
	private string errorMessage = string.Empty;

	private GameObject reportOnA;
	private GameObject reportOnB;

	private void AddToToDoList (Task task, string confirmationSpeech, string errorMessage = "I won't have time for that.") {
		if (numberOfTasks < maxTasks) {
			ToDoList.TODO_LIST.Add (task);
			SET_LIAISON_SPEECH (confirmationSpeech);
			numberOfTasks++;
		}
		else {
			SET_LIAISON_SPEECH (errorMessage);
		}
	}
	
	enum Reports {
		
	};
	
	enum Orders {
		MainMenu,
		TalkTo,
		ErrorMessage,
		ReportOn,
		BringMe,
		ContextualQuestions,
		Interact
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
			// give the player all of Liaison's items
			if (LiaisonInventory.LIAISON_INVENTORY.Count != 0) {
				GUILayout.Box ("Item Delivery");
				if (GUILayout.Button ("Take all items from liaison")) {
					foreach (Ingredient i in LiaisonInventory.LIAISON_INVENTORY) {
						Player.INVENTORY.Add (i);
					}
					LiaisonInventory.LIAISON_INVENTORY.Clear ();
				}
			}


			// reports subheading
			GUILayout.Box ("Reports");
			if (GUILayout.Button ("Report on...")) {
				ordersLocation = Orders.ReportOn;
			}
			if (GUILayoutUtility.GetLastRect().Contains (Event.current.mousePosition)) {
				GAME_MANAGER.ShowToolTip ("Tailing a crewmember can reveal influential new information about them.");
			}
			// orders subheading
			GUILayout.Box ("Orders");
			if (GUILayout.Button ("Talk to...")) {
				ordersLocation = Orders.TalkTo;
			}
			if (GUILayout.Button ("Bring me...")) {
				ordersLocation = Orders.BringMe;
			}
			// gift subheading
			GUILayout.Box ("You and Liaison");
			if (GUILayout.Button ("Interact with liaison...")) {
				ordersLocation = Orders.Interact;
			}
			GUILayout.Box ("Finish Meeting");
			if (GUILayout.Button ("[Submit]")) {
				NEXT_TURN.GO ();
			}
			break;

			
		case Orders.TalkTo:
			foreach (GameObject go in GAME_MANAGER.Roster) {
				if (GUILayout.Button ("..." + go.name + ".")) {
					TalkTo task = new TalkTo(go, string.Format ("Talk to {0}", go.name));
					// TODO: Consolidate task management
					if (ToDoList.TODO_LIST.Contains (task)) {
						SET_LIAISON_SPEECH ( string.Format ("I'm already going to talk to {0}.", go.name));
					}
					else {
						if (numberOfTasks < maxTasks) {
				
							ToDoList.TODO_LIST.Add (task);

							SET_LIAISON_SPEECH (GAME_MANAGER.GET_RESPONSE (go,
							                                             string.Format ("I don't see what good that'll do, but I'll talk to {0}.", go.name),
							                                             string.Format ("There's not going to be much changing their mind, but alright."),
							                                             string.Format ("Alright, I'll talk to {0}.", go.name),
							                                             string.Format ("I think {0} is leaning toward us. That's a good idea.", go.name),
							                                             string.Format ("I'll check in with our friend {0}.", go.name),
							                                             string.Format ("{0} is very much on board already, but I'll stop by anyway.", go.name)));




							//ordersLocation = Orders.MainMenu;
							numberOfTasks++;

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
			foreach (GameObject character in GAME_MANAGER.Roster) {
				if (GUILayout.Button(character.name)) {
					// TODO: Consolidate task management
					Tail follow = new Tail (character, string.Format ("Follow {0}.", character.name));
					AddToToDoList (follow, "I'll try to follow " + character.name + ".");
				}
			}

			MainMenuButton();
			break;
			
		case Orders.BringMe:
			foreach (Ingredient i in CRAFTING_MASTER.BASIC_INGREDIENTS) {
				if (GUILayout.Button ("..." + i.Name + ".")) {
					if (numberOfTasks < maxTasks) {
						BringMe bring = new BringMe (i, string.Format ("Bring me {0}.", i.Name));
						SET_LIAISON_SPEECH (string.Format ("I'll try to bring you {0}.", i.Name.ToLower()));
						ToDoList.TODO_LIST.Add (bring);
						numberOfTasks++;
					}
				}
			}
			MainMenuButton();
			break;

		case Orders.Interact:

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
		Task[] tasks = ToDoList.TODO_LIST.ToArray();
		// TODO: Add tooltip
		foreach (Task s in tasks) {
			if (GUILayout.Button (s.Description)) {
				ToDoList.TODO_LIST.Remove (s);
				numberOfTasks--;
				SET_LIAISON_SPEECH (string.Format ("Okay, I won't {0}{1}.", s.Description[0].ToString().ToLower(), s.Description.Substring(1)));
			}
		}
		GUILayout.EndArea();
	}
	
	
	
	void OnGUI () {
		GUI.skin = skin;
		if (DISPLAYED) {
			DrawEffectsSinceLastTurn();
			DrawLiaisonSpeech();
			DrawDialogue();
			DrawLiaisonToDoList();
		}
	}
}