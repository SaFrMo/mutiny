using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInitiation : MonoBehaviour {

	// GAME INITIATION
	// =================
	// IMPLEMENTATION: Attach to GameManager object. Drag GO containing all crew prefabs to AvailableCrew
	// and change RosterSize accordingly.

	public GameObject AvailableCrew;
	public int rosterSize = 3;

	void Start () {

		// populate list of available crew
		List<GameObject> crewPool = new List<GameObject>();
		foreach (Transform t in AvailableCrew.transform) {
			crewPool.Add (t.gameObject);
		}

		// populate Roster with {rosterSize} characters
		for (int i = 0; i < rosterSize; i++) {
			bool done = false;
			while (!done) {
				GameObject tempCrew = crewPool[UnityEngine.Random.Range (0, crewPool.Count)];
				if (!GAME_MANAGER.Roster.Contains (tempCrew)) {
					GAME_MANAGER.Roster.Add (tempCrew);
					done = true;
				}
			}
		}

		// write names for testing purposes
		foreach (GameObject go in GAME_MANAGER.Roster) {
			Debug.Log (go.name);

			// record if there are any people the character specifically likes or dislikes
			RelationshipInitiation.INITIALIZE_MY_RELATIONSHIPS (go);


		}


	}





}
