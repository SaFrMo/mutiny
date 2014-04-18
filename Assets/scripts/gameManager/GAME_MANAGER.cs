﻿using UnityEngine;
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
	public GUISkin skin;
	private void TutorialCell (string message, float rectX = 10f, float rectY = 200f, float rectWidth = 300f, float rectHeight = 200f) {
		GUI.BeginGroup (new Rect (rectX, rectY, rectWidth, rectHeight));
		GUI.Box (new Rect (0, 0, rectWidth, rectHeight * 2f / 3f), message);
		if (GUI.Button (new Rect (0, rectHeight * 2 / 3, rectWidth / 2, rectHeight / 3), "Previous")) {
			TUTORIAL_PROGRESS--;
		}
		if (GUI.Button (new Rect (rectWidth / 2, rectHeight * 2 / 3, rectWidth / 2, rectHeight / 3), "Next")) {
			TUTORIAL_PROGRESS++;
		}
		GUI.EndGroup();
	}
	public static bool TUTORIAL = true;
	private bool drawn = false;
	public static int TUTORIAL_PROGRESS = 0;
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
