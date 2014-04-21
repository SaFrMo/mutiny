using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Notification {
	protected string _message;
	public string Message {
		get { return _message; }
		set { _message = value; }
	}
}

public class RelationshipChange : Notification {

	public RelationshipChange (string message) {
		_message = message;
	}
/*
	public new string Message {
		get { return _message; }
		set { _message = value; }
	}
	*/
}

public class TurnNotifications : MonoBehaviour {

	public static List<Notification> Updates = new List<Notification>();

	private static bool updatesDisplayed = false;
	private static Vector2 scrollPos;

	public static void SHOW_UPDATES () {
		GUILayout.BeginArea (new Rect (GAME_MANAGER.SPACER, Screen.height * .5f, Screen.width * .25f, Screen.height * .25f));
		//scrollPos = GUILayout.BeginScrollView (scrollPos, GUIStyle.none);
		//if (updatesDisplayed) {
		Notification[] notificationArray = Updates.ToArray();
		foreach (Notification n in notificationArray) {
			print (n.Message);
			if (GUILayout.Button (n.Message)) {
				Updates.Remove (n);
			}
		}
		//}
		//GUILayout.EndScrollView();
		GUILayout.EndArea();
	}
}
