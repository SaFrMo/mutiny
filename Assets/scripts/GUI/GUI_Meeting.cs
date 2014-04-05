using UnityEngine;
using System.Collections;

public class GUI_Meeting : MonoBehaviour {

	public GUISkin skin;

	// TODO: Clean up rects in this code

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
		BringMe = 4
	};

	void MainMenuButton () {
		if (GUILayout.Button ("[Go back]")) {
			ordersLocation = 0;
		}
	}

	void DrawDialogue () {
		//GUI.BeginGroup (new Rect (liaisonSpeechWidth, Screen.height * .5f, liaisonSpeechWidth * .5f, liaisonSpeechHeight * 2f));
		GUILayout.BeginArea (new Rect (liaisonSpeechWidth, Screen.height * .5f, liaisonSpeechWidth * .5f, liaisonSpeechHeight * 2f));


		switch (ordersLocation) {

			// main concourse of orders
		case Orders.MainMenu:
			// reports subheading
			GUILayout.Box ("Reports");
			if (GUILayout.Button ("Report on...")) {
				ordersLocation = Orders.ReportOn;
			}
			//orders subheading
			GUILayout.Box ("Orders");
			if (GUILayout.Button ("Talk to...")) {
				ordersLocation = Orders.TalkTo;
			}
			if (GUILayout.Button ("Bring me...")) {
				ordersLocation = Orders.BringMe;
			}
			break;

		case Orders.TalkTo:
			foreach (GameObject go in GAME_MANAGER.Roster) {
				if (GUILayout.Button (go.name + ".")) {
					if (numberOfTasks < maxTasks) {
						if (ToDoList.TODO_LIST.Contains (string.Format ("Talk to {0}.", go.name))) {
							errorMessage = string.Format ("I'm already going to talk to {0}.", go.name);
							ordersLocation = Orders.ErrorMessage;
						}
						else {
							ToDoList.TODO_LIST.Add (string.Format ("Talk to {0}.", go.name));
							ordersLocation = Orders.MainMenu;
							numberOfTasks++;
						}
					}
					else {
						ordersLocation = Orders.ErrorMessage;
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
		GUILayout.BeginArea (new Rect (liaisonSpeechWidth * 1.5f, Screen.height * .5f, liaisonSpeechWidth * .5f, liaisonSpeechHeight * 2f));
		GUILayout.Box ("LIAISON'S TO-DO LIST.");
		string[] tasks = ToDoList.TODO_LIST.ToArray();
		foreach (string s in tasks) {
			if (GUILayout.Button (s)) {
				ToDoList.TODO_LIST.Remove (s);
				numberOfTasks--;
			}
		}
		GUILayout.EndArea();
	}



	void OnGUI () {
		GUI.skin = skin;
		//if (DISPLAYED) {
			DrawLiaisonSpeech();
			DrawDialogue();
			DrawLiaisonToDoList();
		//}
	}
}
