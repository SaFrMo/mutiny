using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GAME_MANAGER : MonoBehaviour {

	public static GameObject PLAYER = null;
	public static GameObject LIAISON = null;
	public static List<GameObject> Roster = new List<GameObject>();
	public static float SPACER = 50f;

	// RELATIONSHIP GRADES
	public enum RelationshipGrade {
		Excellent,
		Good,
		Medium,
		Poor,
		Bad,
		Enemy
	};

	public static int EXCELLENT_RELATIONSHIP = 85;
	public static int GOOD_RELATIONSHIP = 70;
	public static int MEDIUM_RELATIONSHIP = 50;
	public static int POOR_RELATIONSHIP = 35;
	public static int BAD_RELATIONSHIP = 10;

	public static RelationshipGrade GET_MY_RELATIONSHIP_WITH (GameObject otherCharacter) {
		int relationshipPoints = otherCharacter.GetComponent<CharacterCard>().yourRelationship;
		if (relationshipPoints > BAD_RELATIONSHIP) {
			if (relationshipPoints > POOR_RELATIONSHIP) {
				if (relationshipPoints > MEDIUM_RELATIONSHIP) {
					if (relationshipPoints > GOOD_RELATIONSHIP) {
						if (relationshipPoints > EXCELLENT_RELATIONSHIP) { 
							return RelationshipGrade.Excellent; }
						else { return RelationshipGrade.Good; }
					}
					else { return RelationshipGrade.Medium; }
				}
				else { return RelationshipGrade.Poor; }
			}
			else { return RelationshipGrade.Bad; }
		}
		else { return RelationshipGrade.Enemy; }
	}

	// Responses according to relationship grades
	public static string GET_RESPONSE (GameObject otherCharacter, string enemy, string bad, string poor, string medium, string good, string excellent) {
		switch (GET_MY_RELATIONSHIP_WITH (otherCharacter)) {
		case RelationshipGrade.Enemy:
			return enemy;
			break;
		case RelationshipGrade.Bad:
			return bad;
			break;
		case RelationshipGrade.Medium:
			return medium;
			break;
		case RelationshipGrade.Good:
			return good;
			break;
		default:
			return excellent;
			break;
		};
	}
	
	

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
}
