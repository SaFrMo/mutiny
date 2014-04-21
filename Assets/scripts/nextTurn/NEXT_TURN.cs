using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NEXT_TURN : MonoBehaviour {
	
	static GenerateOverallMood g;
	static RandomEvents r;

	private static void NormalRelationshipChange () {
		foreach (GameObject go in GAME_MANAGER.Roster) {
			CharacterCard c = null;
			try { c = go.GetComponent<CharacterCard>(); }
			catch { Debug.Log (string.Format ("{0} doesn't have a Character Card.", go.name)); }
			List<GameObject> relationshipsKeys = new List<GameObject>(c.relationships.Keys);

			foreach (GameObject kv in relationshipsKeys) {
				int change;
				MutinyGame.RelationshipGrade grade = GAME_MANAGER.GET_RELATIONSHIP_GRADE (c.relationships[kv]);

				// decays a poor relationship, improves a good one
				switch (grade) {
					// TODO: Balance these values
				case MutinyGame.RelationshipGrade.Excellent:
					change = 3;
					break;
				case MutinyGame.RelationshipGrade.Good:
					change = 4;
					break;
				case MutinyGame.RelationshipGrade.Medium:
					change = 2;
					break;
				case MutinyGame.RelationshipGrade.Poor:
					change = 0;
					break;
				case MutinyGame.RelationshipGrade.Bad:
					change = -2;
					break;
				case MutinyGame.RelationshipGrade.Enemy:
					change = -4;
					break;
				default:
					change = 1;
					break;
				};

				// apply change
				c.relationships[kv] += change;
			}
		}
	}


	void Start () {
		g = this.GetComponent<GenerateOverallMood>();
		r = this.GetComponent<RandomEvents>();

	}

	public static void GO () {
		NormalRelationshipChange();
		g.NextTurn();
		r.NextTurn();
		TurnNotifications.Updates.Clear ();
		LiaisonInventory.DO_TODO_LIST();
	}
}
