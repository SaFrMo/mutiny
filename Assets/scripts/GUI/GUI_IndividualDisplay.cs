using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_IndividualDisplay : IGUIMenu {

	public override string ButtonName () {
		return "Individual Information";
	}

	public override void Display () {
		DISPLAYED = true;
	}

	public override void Hide () {
		DISPLAYED = false;
	}

	public override string ToolTip ()
	{
		string message = "View other crewmembers' stats and biographies.";
		if (!ShowInNav) {
			message += "\n\nREQUIRES: Note Sheet or Journal.";
		}
		return message;
	}

	public new List<Ingredient> Requirements = new List<Ingredient>() { CRAFTING_MASTER.journal };

	private bool DISPLAYED = false;

	// class members
	public GUISkin skin;
	public float GAME_MANAGER.SPACER = 10f;

	private static GameObject SELECTED_INDIVIDUAL = null;
	private static string DESCRIPTION = string.Empty;
	private static Texture2D PORTRAIT = null;

	public static void SetSelectedIndividual (GameObject which) {
		SELECTED_INDIVIDUAL = which;
		DESCRIPTION = which.GetComponent<CharacterCard>().description;
		PORTRAIT = which.GetComponent<CharacterCard>().portrait;
	}



	void DisplayInformation () {

		// portrait
		GUI.DrawTexture (new Rect (GAME_MANAGER.SPACER, GAME_MANAGER.SPACER, Screen.width * .5f - (GAME_MANAGER.SPACER * 2), Screen.height - GAME_MANAGER.SPACER * 2), PORTRAIT);

		// information
		GUILayout.BeginArea (new Rect (Screen.width * .5f - (GAME_MANAGER.SPACER * 2), Screen.height * .5f + GAME_MANAGER.SPACER, Screen.width * .25f - GAME_MANAGER.SPACER, Screen.height / 2 - GAME_MANAGER.SPACER * 2));
		GUILayout.Box (DESCRIPTION);
		GUILayout.EndArea();

		// access buttons
		GUILayout.BeginArea (new Rect (Screen.width * .75f - GAME_MANAGER.SPACER * 3, GAME_MANAGER.SPACER, Screen.width * .25f - GAME_MANAGER.SPACER, Screen.height - GAME_MANAGER.SPACER * 2));
		foreach (GameObject x in GAME_MANAGER.Roster) {
			if (GUILayout.Button (x.name)) {
				SetSelectedIndividual (x);
			}
		}
		GUILayout.EndArea();
	}




	void OnGUI () {
		GUI.skin = skin;
		if (DISPLAYED) {
			DisplayInformation();
		}
	}
}
