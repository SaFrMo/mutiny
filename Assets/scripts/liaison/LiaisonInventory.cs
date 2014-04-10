using UnityEngine;
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
	public BringMe (Ingredient what, string description) : base (description)  {
		_what = what;
	}

	// MEMBERS
	// ================
	private Ingredient _what;
	public Ingredient What {
		get { return _what; }
		set { _what = value; }
	}

}









public class LiaisonInventory : MonoBehaviour {


	public static void DO_TODO_LIST () {
		// manage each event in the to-do list

	}

	public static List<Ingredient> LIAISON_INVENTORY = new List<Ingredient>();
}
