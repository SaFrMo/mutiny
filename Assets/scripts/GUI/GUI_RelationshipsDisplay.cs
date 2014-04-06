using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GUI_RelationshipsDisplay : MonoBehaviour, IGUIMenu {

	// interface members
	public string ButtonName() {
		return "Social View";
	}

	public void Display () {
		DISPLAYED = true;
	}
	
	public void Hide() {
		// TODO: Add saving functionality
		DISPLAYED = false;
		DestroyAndClearVectorLineReference();
	}

	// class
	public bool DISPLAYED = false;

	public GUISkin skin;
	public float cellWidth = 100f;
	public float cellHeight = 100f;
	public float spacer = 5f;

	private bool ctrlIsDown;
	private GameObject[] toShowOnActivation;

	// Contains a reference to each crewmember and their cell location
	Dictionary<GameObject, Vector3> crewmemberCellLocation = new Dictionary<GameObject, Vector3>();
	Dictionary<GameObject, Vector2[]> crewmemberLinesReference = new Dictionary<GameObject, Vector2[]>();
	Dictionary<GameObject, VectorLine> crewmemberVectorLineReference = new Dictionary<GameObject, VectorLine>();

	void DestroyAndClearVectorLineReference (GameObject exceptThisOne = null) {
		foreach (KeyValuePair<GameObject, VectorLine> kv in crewmemberVectorLineReference) {
			if (kv.Key != exceptThisOne) {
				VectorLine toDestroy = kv.Value;
				VectorLine.Destroy (ref toDestroy);
			}
		}
		crewmemberVectorLineReference.Clear ();
	}
	
	// A character cell will display their name and your relationship with them.
	void CharacterCell (Rect groupPosition, GameObject character) {
		GUI.BeginGroup (groupPosition);
		if (GUI.Button (new Rect (0, 0, cellWidth, cellHeight), string.Format ("{0}\n{1}", character.name, character.GetComponent<CharacterCard>().yourRelationship))) {

			// "Control" key to manipulate single item in list of selected GOs
			if (crewmemberVectorLineReference.ContainsKey (character)) {
				if (!ctrlIsDown && crewmemberVectorLineReference.Count > 1) {
					DestroyAndClearVectorLineReference();
					VectorLine line = new VectorLine (character.name, crewmemberLinesReference[character], null, 1f);
					crewmemberVectorLineReference.Add (character, line);
					line.Draw();
				}
				else {
					VectorLine toDestroy = crewmemberVectorLineReference[character];
					VectorLine.Destroy (ref toDestroy);
					crewmemberVectorLineReference.Remove (character);
				}
			}
			else {
				if (!ctrlIsDown) {
					DestroyAndClearVectorLineReference(character);
				}
				VectorLine line = new VectorLine (character.name, crewmemberLinesReference[character], null, 1f);
				crewmemberVectorLineReference.Add (character, line);
				line.Draw();
			}
		}
		GUI.EndGroup();
	}



	void DrawRelationshipCircle () {
		for (int i = 0; i < GAME_MANAGER.Roster.Count; i++) {
			Vector2 cellLocation = new Vector2 (crewmemberCellLocation[GAME_MANAGER.Roster[i]].x, crewmemberCellLocation[GAME_MANAGER.Roster[i]].y);
			CharacterCell (new Rect (cellLocation.x - cellWidth / 2, cellLocation.y - cellHeight / 2, cellWidth, cellHeight), GAME_MANAGER.Roster[i]);
		}
	}

	private float relationshipNumbersSize = 50f;

	void DrawRelationshipNumbers () {
		// TODO: prevent multiple draws
		foreach (GameObject source in crewmemberVectorLineReference.Keys) {
			foreach (GameObject other in GAME_MANAGER.Roster) {
				if (source != other) {
					Vector3 sourceLocation = crewmemberCellLocation[source];
					Vector3 otherLocation = crewmemberCellLocation[other];
					Vector3 average = (sourceLocation + otherLocation) / 2;
					float averageFeeling = ((source.GetComponent<CharacterCard>().relationships[other] + 
					                        other.GetComponent<CharacterCard>().relationships[source]) / 2);
					// TODO: custom skin
					GUI.Box (new Rect (average.x - relationshipNumbersSize / 2, average.y - relationshipNumbersSize / 2, relationshipNumbersSize,
					                   relationshipNumbersSize), averageFeeling.ToString ());
				}
			}
		}
	}

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
		}
	}

	void Update () {
		ctrlIsDown = Input.GetKey (KeyCode.LeftControl);
	}


	void OnGUI () {
		if (DISPLAYED) {
			GUI.skin = skin;
			DrawRelationshipCircle();
			DrawRelationshipNumbers();
		}
	}
}
