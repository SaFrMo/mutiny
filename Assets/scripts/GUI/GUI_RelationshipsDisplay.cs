using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GUI_RelationshipsDisplay : MonoBehaviour {

	public GUISkin skin;
	public float cellWidth = 100f;
	public float cellHeight = 100f;
	public float spacer = 5f;

	// array of points to connect cells and show relationships; 500 is an arbitrary Big Number
	Vector2[] points = new Vector2[500];
	//VectorLine line; //= new VectorLine ("Relationships", points, null, 1f);

	// These GameObjects will be the "selected" ones - only their information will show
	//List<GameObject> selectedCharacters = new List<GameObject>();

	// Contains a reference to each crewmember and their cell location
	Dictionary<GameObject, Vector3> crewmemberCellLocation = new Dictionary<GameObject, Vector3>();
	Dictionary<GameObject, Vector2[]> crewmemberLinesReference = new Dictionary<GameObject, Vector2[]>();
	Dictionary<GameObject, VectorLine> crewmemberVectorLineReference = new Dictionary<GameObject, VectorLine>();

	// A character cell will display their name and your relationship with them.
	void CharacterCell (Rect groupPosition, GameObject character) {
		GUI.BeginGroup (groupPosition);
		if (GUI.Button (new Rect (0, 0, cellWidth, cellHeight), string.Format ("{0}\n{1}", character.name, character.GetComponent<CharacterCard>().yourRelationship))) {

			// "Control" key to manipulate single item in list of selected GOs
			//if (Event.current.keyCode == KeyCode.LeftControl) {
				/*
				if (selectedCharacters.Contains (character)) {
					selectedCharacters.Remove (character);
				}
				else {
					selectedCharacters.Add (character);
				}
				*/
				//character.GetComponent<CharacterCard>().drawRelationshipLines = !character.GetComponent<CharacterCard>().drawRelationshipLines;
			if (crewmemberVectorLineReference.ContainsKey (character)) {
				VectorLine toDestroy = crewmemberVectorLineReference[character];
				VectorLine.Destroy (ref toDestroy);
				crewmemberVectorLineReference.Remove (character);
			}
			else {
				VectorLine line = new VectorLine (character.name, crewmemberLinesReference[character], null, 1f);
				crewmemberVectorLineReference.Add (character, line);
				line.Draw();
			}
			//}

			// manipulate display of a single character at a time
			/*
			else {
				if (selectedCharacters.Contains (character)) {
					selectedCharacters.Remove (character);
				}
				else {
					selectedCharacters.Add (character);
				}
			}
			*/
		}
		GUI.EndGroup();
	}



	void DrawRelationshipCircle () {
		for (int i = 0; i < GAME_MANAGER.Roster.Count; i++) {
			Vector2 cellLocation = new Vector2 (crewmemberCellLocation[GAME_MANAGER.Roster[i]].x, crewmemberCellLocation[GAME_MANAGER.Roster[i]].y);
			CharacterCell (new Rect (cellLocation.x - cellWidth / 2, cellLocation.y - cellHeight / 2, cellWidth, cellHeight), GAME_MANAGER.Roster[i]);
		}
	}

	/*
	void DrawRelationshipLines (GameObject source) {
		foreach (KeyValuePair<GameObject, int> kv in source.GetComponent<CharacterCard>().relationships) {
			GameObject go = kv.Key;
			if (go != source) {

			}
		}
		//VectorLine line = new VectorLine (source.name, pointsList.ToArray(), null, 1f);
		if (selectedCharacters.Contains (source)) {

		}
		else {
			//VectorLine.Destroy (ref line);
		}

	}
*/

	void Start () {
		// first, grab the list of point Vector3s for the circle
		List<Vector3> points = SaFrMo.DrawCirclePoints (GAME_MANAGER.Roster.Count, 200f, Screen.width / 2, Screen.height / 2);
		
		// then, populate the reference list
		for (int i = 0; i < GAME_MANAGER.Roster.Count; i++) {
			crewmemberCellLocation.Add (GAME_MANAGER.Roster[i], points[i]);
		}
		
		// then, draw the cell in the appropriate location


		// create a VectorLine for each character
		foreach (GameObject source in GAME_MANAGER.Roster) {
			List<Vector2> pointsList = new List<Vector2>();
			foreach (GameObject otherCharacter in GAME_MANAGER.Roster) {
				// don't refer to self
				if (otherCharacter != source) {
					// add this character's cell location
					pointsList.Add (new Vector2 (crewmemberCellLocation[source].x, 
					                             (crewmemberCellLocation[source].y - 2 * (crewmemberCellLocation[source].y - (Screen.height / 2)))));
					// add the other character's cell location
					pointsList.Add (new Vector2 (crewmemberCellLocation[otherCharacter].x, 
					                             crewmemberCellLocation[otherCharacter].y - 2 * (crewmemberCellLocation[otherCharacter].y - (Screen.height / 2))));
				}
			}
			crewmemberLinesReference.Add (source, pointsList.ToArray());
			//VectorLine line = new VectorLine (source.name, pointsList.ToArray(), null, 1f);
		}
	}


	void OnGUI () {
		GUI.skin = skin;
		DrawRelationshipCircle();
	}
}
