using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Task {

	// CONSTRUCTORS
	// ===============
	public Task (string action) {
		_action = action;
	}

	// MEMBERS
	// ===============
	private string _action;
	public string Action {
		get { return _action; }
		set { _action = value; }
	}

}

public class LiaisonInventory : MonoBehaviour {

	// FOR FUTURE REFERENCE: A better way to do this would be to create a Task class and have the appropriate
	// action be a member of that class. 

	public static void DO_TODO_LIST () {
		// manage each event in the to-do list
		foreach (string s in ToDoList.TODO_LIST) {
			if (s.Contains ("Talk to")) {
				print ("talked!");
			}
		}
		ToDoList.TODO_LIST.Clear ();
	}

	public static List<Ingredient> LIAISON_INVENTORY = new List<Ingredient>();
}
