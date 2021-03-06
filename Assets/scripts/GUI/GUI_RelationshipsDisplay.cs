﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class GUI_RelationshipsDisplay : IGUIMenu {
	
	// interface members
	public override string ButtonName() {
		return "Social View";
	}
	
	public override void Display () {
		DISPLAYED = true;
	}
	
	public override void Hide() {
		// TODO: Add saving functionality
		DISPLAYED = false;
		DestroyAndClearVectorLineReference();
	}

	public override string ToolTip ()
	{
		string message = "View relationships among the crew.";
		if (!ShowInNav) {
			message += "\n\nREQUIRES: Note Sheet or Journal.";
		}
		return message;
	}

	public new List<Ingredient> Requirements = new List<Ingredient>() { CRAFTING_MASTER.journal };

	// class
	public bool DISPLAYED = false;
	
	
	
	
	public GUISkin skin;
	public float cellWidth = 150f;
	public float cellHeight = 150f;
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
		CharacterCard card = character.GetComponent<CharacterCard>();
		GUIContent cellContent = new GUIContent(string.Format ("{0}\n{1}", character.name, card.yourRelationship), card.portrait); 
		GUI.BeginGroup (groupPosition);
		if (GUI.Button (new Rect (0, 0, cellWidth, cellHeight), cellContent)) {
			
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
	private string relationshipSummary = string.Empty;
	
	void DrawRelationshipNumbers () {
		// TODO: prevent multiple draws - RESOLVED?
		foreach (GameObject source in crewmemberVectorLineReference.Keys) {
			foreach (GameObject other in GAME_MANAGER.Roster) {
				if (source != other) {
					Vector3 sourceLocation = crewmemberCellLocation[source];
					Vector3 otherLocation = crewmemberCellLocation[other];
					Vector3 average = (sourceLocation + otherLocation) / 2;
					// prevents multiple draws - places relationship cell in the middle if both are selected, otherwise leans toward source
					//if (!crewmemberVectorLineReference.ContainsKey (other)) {
						// leans toward the source to prevent drawing over each other
						average = Vector3.MoveTowards (average, sourceLocation, crewmemberVectorLineReference[source].GetLength() * .0625f);
					//}

					float averageFeeling;

					// EASY: average feeling
					averageFeeling = ((source.GetComponent<CharacterCard>().relationships[other] + other.GetComponent<CharacterCard>().relationships[source]) / 2);

					// HARD: individual feelings
					//averageFeeling = source.GetComponent<CharacterCard>().relationships[other];


					// TODO: custom skin
					Rect rt = new Rect (average.x - relationshipNumbersSize / 2, average.y - relationshipNumbersSize / 2, relationshipNumbersSize,
					                    relationshipNumbersSize);
					GUI.Box (rt, averageFeeling.ToString (), skin.customStyles[1]);
					// TODO: Cache relationship summaries and prevent slowdowns
					if (rt.Contains (Event.current.mousePosition)) {
						if (string.IsNullOrEmpty (relationshipSummary))
							relationshipSummary = Content.GetRelationshipStatus(averageFeeling, source.name, other.name);
						GAME_MANAGER.ShowToolTip (relationshipSummary);
					}

					else {
						if (!string.IsNullOrEmpty(relationshipSummary)) {
							relationshipSummary = null;
						}
					}
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
