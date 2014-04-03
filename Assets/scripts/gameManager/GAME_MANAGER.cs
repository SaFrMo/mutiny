using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GAME_MANAGER : MonoBehaviour {

	public static GameObject PLAYER = null;
	public static GameObject LIAISON = null;
	public static List<GameObject> Roster = new List<GameObject>();

	// Use this for initialization
	void Start () {
		if (PLAYER == null) {
			if ((PLAYER = GameObject.Find ("Player")) == null) {
				print ("Couldn't find player!");
			}
		}
		if (LIAISON == null) {
			if ((LIAISON = GameObject.Find ("Liaison")) == null) {
				print ("Couldn't find liaison!");
			}
		}
	}

	// TODO: Remove. Testing purposes only.
	void OnGUI () {
		if (GUI.Button (new Rect(0, 0, 100f, 100f), "Next Turn")) {
			NEXT_TURN.GO ();
		}
	}
}
