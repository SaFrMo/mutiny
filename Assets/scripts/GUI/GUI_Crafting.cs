using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_Crafting : IGUIMenu {

	// inherited members
	private bool DISPLAYED = false;

	public override string ButtonName () {
		return "Item Creation";
	}

	public override void Hide () {
		DISPLAYED = false;
	}

	public override void Display () {
		DISPLAYED = true;
	}

	// class members
	public GUISkin skin;

	private float inventoryWidth;
	private float inventoryHeight;

	private float cellSide;
	private float cellsPerRow = 5f;

	void Start () {
		inventoryWidth = Screen.width * .25f;
		inventoryHeight = Screen.height * .45f;
		cellSide = inventoryWidth / cellsPerRow;
	}

	void DrawCell (string objectName, Texture2D portrait) {
		if (GUILayout.Button (objectName, GUILayout.Width(cellSide), GUILayout.Height(cellSide))) {
			if (itemsToCraft.Contains (objectName)) {
				itemsToCraft.Remove (objectName);
			}
			else {
				if (itemsToCraft.Count < maxCraftingSize) {
					itemsToCraft.Add (objectName);
				}
				else {
					//TODO: "not enough crafting space" error
				}
			}			
		}
	}

	void DrawInventory () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - inventoryWidth, Screen.height * .25f, inventoryWidth, inventoryHeight));
		for (int currentIndex = 0; currentIndex < Player.INVENTORY.Count; currentIndex++) {
			// start a row at the first item
			if (currentIndex == 0) { GUILayout.BeginHorizontal(); }
			// finish a row at the appropriate item, starting a new one afterwards
			if (currentIndex / cellsPerRow == 1) { GUILayout.EndHorizontal(); GUILayout.BeginHorizontal(); }
			DrawCell (Player.INVENTORY[currentIndex], null);
		}
		// wrap up the last row
		GUILayout.EndHorizontal();
		GUILayout.EndArea();

	}

	void DrawCrafting () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - inventoryWidth, Screen.height * .75f, inventoryWidth / 2, inventoryHeight / 2));

		// representation of itemsToCraft list
		string craftingList = string.Empty;
		foreach (string s in itemsToCraft) {
			craftingList += s + "\n";
		}

		GUILayout.BeginHorizontal();
		GUILayout.Box (craftingList);
		if (GUILayout.Button ("[TRY CRAFTING]")) {
			AttemptCrafting();
		}
		GUILayout.EndHorizontal();

		GUILayout.EndArea();
	}

	// CRAFTING SECTION
	// ==================

	private int maxCraftingSize = 3;
	private List<string> itemsToCraft = new List<string>();

	private bool ListsMatch (List<string> a, List<string> b) {
		// return false if they're not the same size
		if (a.Count != b.Count) {
			return false;
		}
		else {
			foreach (string s in a) {
				if (!b.Contains (s)) {
					return false;
				}
			}
			return true;
		}
	}


	private void AttemptCrafting () {
		int resultIndex = craftingReference.FindIndex (x => ListsMatch (itemsToCraft, x));

		if (resultIndex != null) {
			foreach (string x in itemsToCraft) {
				Player.INVENTORY.Remove (x);
			}
			if (Player.INVENTORY.Count == 0) {
				Player.INVENTORY.Add ("Inventory empty!");
			}
			// TODO: add crafted item
		}
		else {
			// TODO: error message, no such thing
		}
	}

	// MAIN CRAFTING LIST
	// TODO: clean this up. this is very sloppy.
	private List<List<string>> craftingReference = new List<List<string>>() {

		new List<string>() { "1", "2", "3" }, 	// 0
		new List<string>() { "2", "3", "4" } 	// 1
	};


	// ONGUI() SECTION
	// ==================

	void OnGUI () {
		GUI.skin = skin;
		if (DISPLAYED) {
			itemsToCraft.Sort();
			DrawInventory();
			DrawCrafting();
		}
	}
}
