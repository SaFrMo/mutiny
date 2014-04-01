using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GAME_MANAGER : MonoBehaviour {

	public static GameObject PLAYER = null;
	public static List<GameObject> Roster = new List<GameObject>();

	// Use this for initialization
	void Start () {
		if (PLAYER == null) {
			if ((PLAYER = GameObject.Find ("Player")) == null) {
				print ("Couldn't find player!");
			}
		}
	}
}
