using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUI_RelationshipsDisplay : MonoBehaviour {

	public GUISkin skin;
	public float cellWidth = 100f;
	public float cellHeight = 100f;
	public float spacer = 5f;

	// These GameObjects will be the "selected" ones - only their information will show
	List<GameObject> selectedCharacters = new List<GameObject>();

	// A character cell will display their name and your relationship with them.
	void CharacterCell (Rect groupPosition, GameObject character) {
		GUI.BeginGroup (groupPosition);
		if (GUI.Button (new Rect (0, 0, cellWidth, cellHeight), string.Format ("{0}\n{1}", character.name, character.GetComponent<CharacterCard>().yourRelationship))) {
			// TODO: Edit this to mimic InfoAddict functionality
			if (selectedCharacters.Contains (character)) {
				selectedCharacters.Remove (character);
			}
			else 
		}
		GUI.EndGroup();
	}



	void DrawRelationshipCircle () {
		List<Vector3> points = SaFrMo.DrawCirclePoints (GAME_MANAGER.Roster.Count, 200f, Screen.width / 2, Screen.height / 2);
		for (int i = 0; i < GAME_MANAGER.Roster.Count; i++) {
			CharacterCell (new Rect (points[i].x - cellWidth / 2, points[i].y - cellHeight / 2, cellWidth, cellHeight), GAME_MANAGER.Roster[i]);
		}
	}



	void OnGUI () {
		GUI.skin = skin;
		DrawRelationshipCircle();
	}
}
