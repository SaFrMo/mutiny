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
		inventoryWidth = Screen.width * .45f;
		inventoryHeight = Screen.height * .45f;
		cellSide = inventoryWidth / cellsPerRow;
	}

	void DrawCell (string objectName, Texture2D portrait) {
		GUILayout.Box (objectName, GUILayout.Width(cellSide), GUILayout.Height(cellSide));
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

	void OnGUI () {
		GUI.skin = skin;
		if (DISPLAYED) {
			DrawInventory();
		}
	}
}
