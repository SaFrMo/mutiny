using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomEvents : MonoBehaviour {

	// RANDOM EVENTS in MUTINY.
	// ===========================
	// Occasionally, an event will happen between two or more NPCs that will contribute to their relationship in some way.
	// These events occur in the "Preparation" stage, before the player meets with the liaison.
	
	// TO USE: Attach to End Turn GO, then call:
	// GameObject.Find ("End Turn").GetComponent<RandomEvents>().RandomEventApply();

	// two-person events
	Dictionary<int, string> randomEvent = new Dictionary<int, string>() {
		{ 0, "Life Saver" },
		{ 1, "Distant Relation" },
		{ 2, "Shared Religion" }
	};


	// THE MAIN FUNCTION
	// ===================
	public void RandomEventApply () {
		int i = UnityEngine.Random.Range (0, 100);
		if (randomEvent.ContainsKey (i)) {

			// prevent duplicates in games
			randomEvent.Remove (i);

			// who's the first involved?
			int firstPerson = UnityEngine.Random.Range (0, GAME_MANAGER.Roster.Count);
			// who's the second involved? they have to be different than the first
			int secondPerson = firstPerson;
			while (secondPerson == firstPerson) {
				secondPerson = UnityEngine.Random.Range (0, GAME_MANAGER.Roster.Count);
			}

			GameObject a = GAME_MANAGER.Roster[firstPerson];
			GameObject b = GAME_MANAGER.Roster[secondPerson];

			a.GetComponent<CharacterCard>().history.Add (randomEvent[i], b);
			b.GetComponent<CharacterCard>().history.Add (randomEvent[i], a);
		}
	}
}
