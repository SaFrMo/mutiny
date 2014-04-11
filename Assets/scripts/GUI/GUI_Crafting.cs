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
	
	void DrawCell (Ingredient ingredient, Texture2D portrait) {
		if (GUILayout.Button (ingredient.Name, GUILayout.Width(cellSide), GUILayout.Height(cellSide))) {
			if (itemsToCraft.Contains (ingredient)) {
				itemsToCraft.Remove (ingredient);
			}
			else {
				if (itemsToCraft.Count < maxCraftingSize) {
					itemsToCraft.Add (ingredient);
				}
				else {
					//TODO: "not enough crafting space" error
				}
			}			
		}
	}
	
	void DrawInventory () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - inventoryWidth, Screen.height * .25f, inventoryWidth, inventoryHeight));
		if (Player.INVENTORY.Count == 0) { Player.INVENTORY.Add (CRAFTING_MASTER.nothing); }
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
	
	private string resultMessage = string.Empty;
	
	void DrawCrafting () {
		GUILayout.BeginArea (new Rect (Screen.width / 2 - inventoryWidth, Screen.height * .75f, inventoryWidth / 2, inventoryHeight / 2));
		
		// representation of itemsToCraft list
		string craftingList = string.Empty;
		foreach (Ingredient s in itemsToCraft) {
			craftingList += s.Name + "\n";
		}
		
		GUILayout.BeginHorizontal();
		GUILayout.Box (craftingList);
		if (GUILayout.Button ("[TRY CRAFTING]")) {
			Product newProduct = CRAFTING_MASTER.ATTEMPT_CRAFT (itemsToCraft);
			// crafting successful
			if (newProduct != null) { 
				foreach (Ingredient i in itemsToCraft) {
					try {
						if (i.DestroyOnUse) {
							Player.INVENTORY.Remove (i);
						}
					}
					catch { Debug.Log ("Couldn't delete item from inventory: " + i.Name); }
				}
				resultMessage = string.Format ("Created {0}!", newProduct.Name);
				Player.INVENTORY.Add (newProduct);
				itemsToCraft.Clear ();
			}
			else {
				resultMessage = "Couldn't make anything from these!";   
			}
			
		}
		// box containing result/error message
		GUILayout.Box (resultMessage);
		GUILayout.EndHorizontal();
		
		GUILayout.EndArea();
	}
	
	// CRAFTING SECTION
	// ==================
	
	private int maxCraftingSize = 3;
	private List<Ingredient> itemsToCraft = new List<Ingredient>();
	
	
	// ONGUI() SECTION
	// ==================
	
	void OnGUI () {
		GUI.skin = skin;
		if (DISPLAYED) {
			DrawInventory();
			DrawCrafting();
		}
	}
}

