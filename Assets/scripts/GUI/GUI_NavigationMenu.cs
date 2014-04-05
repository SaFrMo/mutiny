using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IGUIMenu
{
	string ButtonName();
	void Display();
	void Hide();
}

public class GUI_NavigationMenu : MonoBehaviour {

	public GUISkin skin;

	// IMPLEMENTATION: Attach to GUI GameObject

	List<IGUIMenu> menus = new List<IGUIMenu>();
	IGUIMenu[] menuArray;

	void Start () {
		// TODO: There must be a more flexible way to populate this list
		IGUIMenu a = GetComponent<GUI_StatsDisplay>();
		IGUIMenu b = GetComponent<GUI_Meeting>();
		IGUIMenu c = GetComponent<GUI_RelationshipsDisplay>();
		menus.Add (a);
		menus.Add (b);
		menus.Add (c);

		menuArray = menus.ToArray();
	}

	void OnGUI () {
		GUI.skin = skin;

		GUILayout.BeginArea (new Rect (Screen.width * .125f, 0, Screen.width * .75f, Screen.height * .125f));
		GUILayout.BeginHorizontal ();

		// draw all buttons
		foreach (IGUIMenu x in menuArray) {

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
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
