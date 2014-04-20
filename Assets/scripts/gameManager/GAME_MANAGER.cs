using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MutinyGame;

namespace MutinyGame {
	// RELATIONSHIP GRADES
	public enum RelationshipGrade {
		Excellent,
		Good,
		Medium,
		Poor,
		Bad,
		Enemy
	};
}

public class GAME_MANAGER : MonoBehaviour {

	public static GameObject PLAYER = null;
	public static GameObject LIAISON = null;
	public static List<GameObject> Roster = new List<GameObject>();
	public static float SPACER = 50f;



	public static int EXCELLENT_RELATIONSHIP = 85;
	public static int GOOD_RELATIONSHIP = 70;
	public static int MEDIUM_RELATIONSHIP = 50;
	public static int POOR_RELATIONSHIP = 35;
	public static int BAD_RELATIONSHIP = 10;

	// player and character
	public static RelationshipGrade GET_MY_RELATIONSHIP_WITH (GameObject otherCharacter) {
		int relationshipPoints = otherCharacter.GetComponent<CharacterCard>().yourRelationship;
		if (relationshipPoints > BAD_RELATIONSHIP) {
			if (relationshipPoints > POOR_RELATIONSHIP) {
				if (relationshipPoints > MEDIUM_RELATIONSHIP) {
					if (relationshipPoints > GOOD_RELATIONSHIP) {
						if (relationshipPoints > EXCELLENT_RELATIONSHIP) { 
							return RelationshipGrade.Excellent; }
						else { print ("g"); return RelationshipGrade.Good; }
					}
					else { print ("m"); return RelationshipGrade.Medium; }
				}
				else { print ("p"); return RelationshipGrade.Poor; }
			}
			else { return RelationshipGrade.Bad; }
		}
		else { return RelationshipGrade.Enemy; }
	}

	// relationship grader between 2 characters
	public static RelationshipGrade GET_RELATIONSHIP_GRADE (GameObject source, GameObject otherCharacter) {
		int relationshipPoints = source.GetComponent<CharacterCard>().relationships[otherCharacter];
		if (relationshipPoints > BAD_RELATIONSHIP) {
			if (relationshipPoints > POOR_RELATIONSHIP) {
				if (relationshipPoints > MEDIUM_RELATIONSHIP) {
					if (relationshipPoints > GOOD_RELATIONSHIP) {
						if (relationshipPoints > EXCELLENT_RELATIONSHIP) { 
							return RelationshipGrade.Excellent; }
						else { print ("g"); return RelationshipGrade.Good; }
					}
					else { print ("m"); return RelationshipGrade.Medium; }
				}
				else { print ("p"); return RelationshipGrade.Poor; }
			}
			else { return RelationshipGrade.Bad; }
		}
		else { return RelationshipGrade.Enemy; }
	}

	// where float points are available
	public static RelationshipGrade GET_RELATIONSHIP_GRADE (float points) {
		int relationshipPoints = (int)points;
		if (relationshipPoints > BAD_RELATIONSHIP) {
			if (relationshipPoints > POOR_RELATIONSHIP) {
				if (relationshipPoints > MEDIUM_RELATIONSHIP) {
					if (relationshipPoints > GOOD_RELATIONSHIP) {
						if (relationshipPoints > EXCELLENT_RELATIONSHIP) { 
							return RelationshipGrade.Excellent; }
						else { return RelationshipGrade.Good; }
					}
					else { return RelationshipGrade.Medium; }
				}
				else { return RelationshipGrade.Poor; }
			}
			else { return RelationshipGrade.Bad; }
		}
		else { return RelationshipGrade.Enemy; }
	}

	// Responses according to relationship grades
	public static string GET_RESPONSE (GameObject otherCharacter, string enemy, string bad, string poor, string medium, string good, string excellent) {
		switch (GET_MY_RELATIONSHIP_WITH (otherCharacter)) {
		case RelationshipGrade.Enemy:
			return enemy;
			break;
		case RelationshipGrade.Poor:
			return poor;
			break;
		case RelationshipGrade.Bad:
			return bad;
			break;
		case RelationshipGrade.Medium:
			return medium;
			break;
		case RelationshipGrade.Good:
			return good;
			break;
		default:
			return excellent;
			break;
		};
	}

	// Tooltip system
	public static void ToolTipBox (string message) {
		GUI.Box (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, 300f, 100f), message);
	}

	private static void WindowFun (int id) {}
	public static void ShowToolTip (string message) {
		GUI.Window (0, new Rect (Input.mousePosition.x + 10f, SaFrMo.InputYToGUIY(Input.mousePosition.y), 300f, 150f), WindowFun, message);
		GUI.BringWindowToFront(0);
	}
	// standalone - place anywhere
	public static void ShowToolTip (string message, Rect reference) {
		if (reference.Contains(Event.current.mousePosition)) {
			ShowToolTip (message);
		}
	}

	// TUTORIAL FUNCTIONS
	// ======================================
	public GUISkin skin;
	private void TutorialCell (string message, float rectX = 10f, float rectY = 200f, float rectWidth = 300f, float rectHeight = 200f, bool drawButtons = true) {
		GUI.BeginGroup (new Rect (rectX, rectY, rectWidth, rectHeight));
		GUI.Box (new Rect (0, 0, rectWidth, rectHeight * 2f / 3f), message);
		if (drawButtons) {
			if (GUI.Button (new Rect (0, rectHeight * 2 / 3, rectWidth / 2, rectHeight / 3), "Previous")) {
				TUTORIAL_PROGRESS--;
			}
			if (GUI.Button (new Rect (rectWidth / 2, rectHeight * 2 / 3, rectWidth / 2, rectHeight / 3), "Next")) {
				TUTORIAL_PROGRESS++;
			}
		}
		GUI.EndGroup();
	}
	public static bool TUTORIAL = true;
	IGUIMenu currentWindow = null;
	private bool drawn = false;
	public int TUTORIAL_PROGRESS = 0;
	private void RunTutorial () {
		// TODO: TUTORIAL
		// tutorial's always at the front
		//GUI.depth = 0;
		switch (TUTORIAL_PROGRESS) {
		case 0:
			TutorialCell ("Welcome to the Mutiny tutorial! Click \"Next\" to begin learning how to play Mutiny. Toggle the tutorial on or off in the pause menu (ESC).");
			break;

		case 1:
			TutorialCell ("You have been imprisoned by your erratic and unstable captain, and will need the help of your liaison to orchestrate a mutiny.");
			break;

		case 2:
			TutorialCell ("This is the Meeting window, where you will interact with your liaison directly.");
			break;

		case 3:
			TutorialCell ("Your liaison's speech to you is visible here; he will also report to you on the progress of your orders and requests.",
			              10, Screen.height * .5f);
			break;

		case 4:
			TutorialCell ("You can make orders and requests here. Your liaison has normal crew duties on top of assisting you, and so will only be able to complete a certain number of actions per turn.",
			              Screen.width / 2);
			break;

		case 5:
			TutorialCell ("You can check up on your inventory here - either by clicking the \"Inventory\" tab or pressing TAB.",
			              Screen.width * 3 / 4);
			break;

		case 6:
			if (currentWindow == null) {
				currentWindow = GameObject.Find ("GUI").GetComponent<GUI_Crafting>();
			}
			if (!(currentWindow as GUI_Crafting).GetDisplayed()) {
				TutorialCell ("To use items in your inventory, click on \"Item Creation\" in the navigation bar.",
				              Screen.width - 300, 100f, 300, 200, false);
			}
			else {
				TutorialCell ("In this window, you can craft your basic items into more complicated - and more useful - ones.");
			}
			break;

		case 7:
			TutorialCell ("Let's start with a very practical item - the Note Sheet. You can use Note Sheets to unlock more windows in the Navigation Bar, which will help you plan your coup more effectively.",
			              Screen.width / 2 - 300 / 2, 50f);
			if (currentWindow != null) {
				currentWindow = null;
			}
			break;

		case 8:
			TutorialCell ("You start the game knowing how to make a note sheet, so the required materials are listed under the tab on the left side of the screen.",
			              Screen.width / 2 - 300 / 2, 50f);
			break;

		case 9:
			TutorialCell ("Click on \"Note Sheet\" to view the required materials to craft a Note Sheet, then click \"Next\" to continue.", Screen.width * .25f);
			break;

		case 10:
			TutorialCell ("You have a leaf of paper and a quill already, so click on each item to add it to construction.",
			              10, 200, 300, 200, false);
			// TODO: will this suck up too much memory?
			if (CRAFTING_MASTER.ListsMatch (GameObject.Find ("GUI").GetComponent<GUI_Crafting>().itemsToCraft, CRAFTING_MASTER.noteSheet.Recipe)) {
				TUTORIAL_PROGRESS++;
			}
			break;

		case 11:
			TutorialCell ("Good! Click on \"Try Crafting\" to create the note sheet.",
			              10, 200, 300, 200, false);
			if (Player.INVENTORY.Contains (CRAFTING_MASTER.noteSheet)) {
				TUTORIAL_PROGRESS++;
			}
			break;

		case 12:
			TutorialCell ("Excellent work! Be creative in your crafting. You can discover new recipes on your own, learn about new recipes from your liaison or " +
			              "other crewmembers, and more.");
			break;

		case 13: 
			TutorialCell ("Now, use this note sheet to unlock the Social View, one of the most important windows in Mutiny. When it's unlocked, clicked" +
				"\"Social View\" to proceed.",
			              10, 200, 300, 200, false);
			if (currentWindow == null) {
				currentWindow = GameObject.Find ("GUI").GetComponent<GUI_RelationshipsDisplay>();
			}
			if ((currentWindow as GUI_RelationshipsDisplay).DISPLAYED) {
				TUTORIAL_PROGRESS++;
			}
			break;

		case 14:
			TutorialCell ("Click on any crewmember's portrait to view their relationship with the rest of the crew. Their feelings toward you and your liaison are show in the " +
			              "box that contains their portrait.");
			break;

		case 15:
			TutorialCell ("The higher the numbers here, the better the relationship. People on either extreme, for your endeavour or against it, tend to be pretty set in their views, " +
			              "but those in the middle of the spectrum are more malleable.");
			break;

		case 16:
			TutorialCell ("Use gifts, bribes, blackmail, threats, promises, appeals to religion, appeals to logic, and more to sway those around you. The more varied your liaison's " +
			              "activities, the more varied your approach can be.");
			break;

		case 17:
			TutorialCell ("You now have enough information to begin sowing the seeds of discontent! Consult the Reference Guide for more information, and good luck!");
			break;

		case 18:
			TUTORIAL = false;
			break;

		default:
			TUTORIAL_PROGRESS = 0;
			break;
		};

	}

	void OnGUI() {
		GUI.skin = skin;
		//if (TUTORIAL) {
			RunTutorial();
		//}
	}
	
	

	// Use this for initialization
	void Start () {
		if (PLAYER == null) {
			if ((PLAYER = GameObject.Find ("Player")) == null) {
				print ("Couldn't find player!");
			}
		}
		if (LIAISON == null) {
			if ((LIAISON = GameObject.Find ("Liaison")) == null) {
				print ("Couldn't find liaison!");
			}
		}
	}
}
