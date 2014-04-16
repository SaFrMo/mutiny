using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IGUIMenu : MonoBehaviour
{
	public virtual string ButtonName() { return string.Empty; }
	public bool ShowInNav = false;
	public virtual string Requirements() { return string.Empty; }
	public virtual void Display() {}
	public virtual void Hide() {}
	public virtual string ToolTip () { return string.Empty; }
}

public class GUI_NavigationMenu : MonoBehaviour {

	public GUISkin skin;

	// IMPLEMENTATION: Attach to GUI GameObject

	private List<IGUIMenu> menus = new List<IGUIMenu>();
	private IGUIMenu[] menuArray;

	void Start () {
		// TODO: There must be a more flexible way to populate this list
		menus = new List<IGUIMenu>() {
			GetComponent<GUI_StatsDisplay>(),
			GetComponent<GUI_Meeting>(),
			GetComponent<GUI_RelationshipsDisplay>(),
			GetComponent<GUI_IndividualDisplay>(),
			GetComponent<GUI_Crafting>()
		};
		/*
		IGUIMenu a = GetComponent<GUI_StatsDisplay>();
		IGUIMenu b = GetComponent<GUI_Meeting>();
		IGUIMenu c = GetComponent<GUI_RelationshipsDisplay>();
		IGUIMenu d = GetComponent<GUI_IndividualDisplay>();
		menus.Add (a);
		menus.Add (b);
		menus.Add (c);
		menus.Add (d);
		*/

		menuArray = menus.ToArray();

		// this will display the default screen for a new game (the Meeting screen)
		foreach (IGUIMenu x in menus) {
			if (x == GetComponent<GUI_Meeting>()) {
				x.Display();
			}
			else {
				x.Hide ();
			}
		}
	}

	public static Rect INVENTORY_LOCATION;

	void OnGUI () {
		GUI.skin = skin;

		GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height * .5f));
		GUILayout.BeginHorizontal ();



		// draw all buttons
		foreach (IGUIMenu x in menuArray) {
			// ...as long as they're marked "drawable" - use this to gradually increase the complexity of the game
			if (x.ShowInNav) {
				// if button is clicked...
				if (GUILayout.Button (x.ButtonName())) {
					// cycle through all IGUIMenus and make sure selected one isn't the clicked one
					foreach (IGUIMenu y in menus) {
						if (y == x) {
							y.Display();
						}
						else {
							y.Hide ();
						}
					}
				}
			}
			else {
				// no options available
				if (!Player.INVENTORY.Contains (CRAFTING_MASTER.noteSheet)) {
					GUILayout.Box (x.ButtonName());
				}
				// option to unlock a screen
				else {
					if (GUILayout.Button (x.ButtonName() + "\nClick to spend 1 [Note Sheet] and unlock")) {
						x.ShowInNav = true;
						Player.INVENTORY.Remove (CRAFTING_MASTER.noteSheet);
					}
				}
				// TODO: This should be more flexible - checks to see if requirements for unlocking x menu are satisfied
				// journal unlocks everything
				if (Player.INVENTORY.Contains (CRAFTING_MASTER.journal)) {
					x.ShowInNav = true;
				}
			}
			// shows what you could have
			Rect rt = GUILayoutUtility.GetLastRect();//Rect(new GUIContent(x.ButtonName()), GUIStyle.none);
			if (rt.Contains (Event.current.mousePosition)) {
				GAME_MANAGER.ShowToolTip(x.ToolTip());
			}

		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
