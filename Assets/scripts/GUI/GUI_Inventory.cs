using UnityEngine;
using System.Collections;

public class GUI_Inventory : MonoBehaviour {

	public GUISkin skin;

	public KeyCode inventoryHotkey = KeyCode.I;

	private float alpha = 0;
	private float buttonWidth = 100f;
	private float buttonHeight = 100f;
	private float inventoryWidth = 300f;
	private float inventoryHeight = 500f;
	private float currentInventoryWidth = 0;
	private float resizeRate = 25f;

	private bool showInventory = false;

	private void InventoryHotkey() {
		if (Input.GetKeyDown (inventoryHotkey)) {
			showInventory = !showInventory;
		}
	}
	

	private void DrawInventoryButton () {
		Rect buttonLocation = new Rect (Screen.width - buttonWidth - currentInventoryWidth, Screen.height / 2 - buttonHeight, buttonWidth, buttonHeight);

		if (GUI.Button (buttonLocation, "I\nN\nV")) {
			showInventory = !showInventory;
		}
	}

	private void DrawInventoryWindow () {
		if (showInventory) {
			// expand the window if applicable
			if (currentInventoryWidth < inventoryWidth) {
				currentInventoryWidth += resizeRate;
			}
		}
		else {
			// contract the window if applicable
			if (currentInventoryWidth > 0) {
				currentInventoryWidth -= resizeRate;
			}
		}
		// draw the window itself
		GUILayout.BeginArea (new Rect (Screen.width - currentInventoryWidth, Screen.height / 2 - buttonHeight, inventoryWidth, inventoryHeight));
		GUILayout.Box ("test");
		GUILayout.EndArea();

	}

	void Update () {
		InventoryHotkey();
	}

	void OnGUI () {
		GUI.skin = skin;
		DrawInventoryButton();
		DrawInventoryWindow();
	}
}
