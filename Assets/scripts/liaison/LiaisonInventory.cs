﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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









public class LiaisonInventory : MonoBehaviour {

	private static void SmuggleIn (BringMe what) {
		float aleaIactaEst = UnityEngine.Random.Range (0, 100);
		if (aleaIactaEst <= what.SuccessRate) {
			LIAISON_INVENTORY.Add (what.What); // HAH
			// TODO: More variety in messages
			GUI_Meeting.SET_LIAISON_SPEECH ("I brought you a few things you wanted.", false);
		}
		else {
			// TODO: more variety, natural quality to speech
			GUI_Meeting.SET_LIAISON_SPEECH ("I wasn't able to get a couple things.", false);
		}
	}


	public static void DO_TODO_LIST () {
		// manage each event in the to-do list
		foreach (Task t in ToDoList.TODO_LIST) {
			if (t is BringMe) {
				SmuggleIn (t as BringMe);
			}
			else if (t is TalkTo) {
				// TODO: talking code
			}
			else {
				Debug.Log ("Task not recognized: " + t.Description);
			}
		}
		// clear the to-do list
		ToDoList.TODO_LIST.Clear ();
	}

	public static List<Ingredient> LIAISON_INVENTORY = new List<Ingredient>();
}
