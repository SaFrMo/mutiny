﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MutinyGame;

public class Task {

	public Task (string description) {
		_description = description;
	}

	private string _description;
	public string Description {
		get { return _description; }
		set { _description = value; }
	}
}

public class TalkTo : Task {
	// CONSTRUCTORS
	// ================
	public TalkTo (GameObject whom, string description) : base (description) {
		_whom = whom;
	}

	// MEMBERS
	// ================
	private GameObject _whom;
	public GameObject Whom {
		get { return _whom; }
		set { _whom = value; }
	}
}

public class BringMe : Task {
	// CONSTRUCTORS
	// ================
	public BringMe (Ingredient what, string description, float successRate = 0.8f) : base (description)  {
		_what = what;
		_successRate = successRate;
	}

	// MEMBERS
	// ================
	private Ingredient _what;
	public Ingredient What {
		get { return _what; }
		set { _what = value; }
	}

	private float _successRate;
	public float SuccessRate {
		get { return _successRate * 100f; }
		set { _successRate = value; }
	}

}

public class Tail : Task {
	// CONSTRUCTORS
	public Tail (GameObject whom, string description, float successRate = .75f) : base (description) {
		_whom = whom;
		_successRate = successRate;
	}

	// MEMBERS
	private GameObject _whom;
	public GameObject Whom {
		get { return _whom; }
		set { _whom = value; }
	}

	private float _successRate;
	public float SuccessRate {
		get { return _successRate * 100f; }
		set { _successRate = value; }
	}
}









public class LiaisonInventory : MonoBehaviour {

	private static void SmuggleIn (BringMe what) {
		float aleaIactaEst = UnityEngine.Random.Range (0, 100);
		if (aleaIactaEst <= what.SuccessRate) {
			LIAISON_INVENTORY.Add (what.What); // HAH
			// TODO: More variety in messages
			GUI_Meeting.SET_LIAISON_SPEECH ("I brought you " + what.What.Name + ".", false);
		}
		else {
			// TODO: more variety, natural quality to speech
			GUI_Meeting.SET_LIAISON_SPEECH ("I wasn't able to get a couple things.", false);
		}
	}

	private static void Follow (Tail whom) {
		float aleaIactaEst = UnityEngine.Random.Range (0, 100);
		if (aleaIactaEst <= whom.SuccessRate) {
			//Ingredient gossip = new Ingredient ("Information on " + whom.Whom.name, false);
			//GUI_Meeting.SET_LIAISON_SPEECH (
			CRAFTING_MASTER.CreateGossip (whom.Whom);
		}
		else {
			// TODO: more variety, natural quality to speech
			GUI_Meeting.SET_LIAISON_SPEECH ("No luck on following " + whom.Whom.name + ".", false);
		}
	}


	private static void Talk (TalkTo whom) {
		CharacterCard card = whom.Whom.GetComponent<CharacterCard>();
		// TODO: Balance these values
		switch (GAME_MANAGER.GET_RELATIONSHIP_GRADE(whom.Whom.GetComponent<CharacterCard>().yourRelationship)) {
		case RelationshipGrade.Excellent:
			card._YourRelationship += 2;
			break;
		case RelationshipGrade.Good:
			card._YourRelationship += 3;
			break;
		case RelationshipGrade.Medium:
			card._YourRelationship += 7;
			break;
		case RelationshipGrade.Poor:
			card._YourRelationship += 4;
			break;
		default:
			card._YourRelationship += 1;
			break;
		};
	}


	public static void DO_TODO_LIST () {
		// manage each event in the to-do list
		foreach (Task t in ToDoList.TODO_LIST) {
			if (t is BringMe) {
				SmuggleIn (t as BringMe);
			}
			else if (t is TalkTo) {
				Talk (t as TalkTo);
			}
			else if (t is Tail) {
				Follow (t as Tail);
			}
			else {
				Debug.Log ("Task not recognized: " + t.Description);
			}
		}
		// clear the to-do list
		ToDoList.TODO_LIST.Clear ();
		GUI_Meeting.numberOfTasks = 0;
	}

	public static List<Ingredient> LIAISON_INVENTORY = new List<Ingredient>();
}
