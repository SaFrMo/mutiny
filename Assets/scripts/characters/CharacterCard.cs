using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MutinyGame;

public class CharacterCard : MonoBehaviour {

	public string characterName;
	public int personalityType;
	public Dictionary<GameObject, int> relationships = new Dictionary<GameObject, int>();
	private int _yourRelationship;
	public int yourRelationship {
		get { return _yourRelationship; }
		set { _yourRelationship = value; }
	}

	public int _YourRelationship {
		get { return _yourRelationship; }
		set { 
			int difference = value - _yourRelationship;
			_yourRelationship = value;
			TurnNotifications.Updates.Add (new RelationshipChange (string.Format ("Your relationship with {0} {1} {2} point(s).",
			                                                                      characterName,
			                                                                      (value > 0 ? "went up by" : "went down by"),
			                                                                      Mathf.Abs (difference).ToString())));
		}
	}

	public RelationshipGrade YourRelationship {
		get { return GAME_MANAGER.GET_RELATIONSHIP_GRADE (yourRelationship); }
	}


	public Texture2D portrait;
	public bool drawRelationshipLines = false;
	public Dictionary<string, GameObject> history = new Dictionary<string, GameObject>();
	public string description;
}
