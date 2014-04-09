using UnityEngine;
using System.Collections;

public class GUI_Inventory : MonoBehaviour {

	private void DrawInventoryButton () {
		if (Input.mousePosition.y <= 0) {
			// TODO: Scoot everything up X pixels?
		}
	}

	void OnGUI () {
		DrawInventoryButton();
	}
}
