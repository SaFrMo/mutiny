﻿using UnityEngine;
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

	private List<IGUIMenu> menus = new List<IGUIMenu>();
	private IGUIMenu[] menuArray;

	void Start () {
		// TODO: There must be a more flexible way to populate this list
		menus = new List<IGUIMenu>() {
			GetComponent<GUI_StatsDisplay>(),
			GetComponent<GUI_Meeting>(),
			GetComponent<GUI_RelationshipsDisplay>(),
			GetComponent<GUI_IndividualDisplay>()
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
				x.Display ();
			}
			else {
				x.Hide ();
			}
		}
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
