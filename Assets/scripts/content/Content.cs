using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MutinyGame;

public static class Content {

	// GUI_RelationshipsDisplay
	// ===========================

	// Summary of relationship status between two characters, based on the cell value in the social window.
	public static string GetRelationshipStatus (float averageFeeling, string source, string other) {

		// HARD: Don't refer to their relationship as an average of two values

		// subject
		string clause1 = string.Format ("{0} and {1} ", source, other);

		// predicates
		List<string> predicates;
		string predicate = string.Empty;

		switch (GAME_MANAGER.GET_RELATIONSHIP_GRADE(averageFeeling)) {

		case RelationshipGrade.Excellent:
			predicates = new List<string>() {
				"are joined at the hip."
			};
			break;

		case RelationshipGrade.Good:
			predicates = new List<string>() {
				"are good friends."
			};
			break;

		case RelationshipGrade.Medium:
			predicates = new List<string>() {
				"have no quarrel."
			};
			break;

		case RelationshipGrade.Poor:
			predicates = new List<string>() {
				"usually avoid each other."
			};
			break;

		case RelationshipGrade.Bad:
			predicates = new List<string>() {
				"are outright foes."
			};
			break;

		default:
			predicates = new List<string>() {
				"I'm ambivalent."
			};
			break;	

		};


		string toReturn = clause1 + predicates[UnityEngine.Random.Range (0, predicates.Count)];
		return toReturn;

		return string.Empty;

	}
}
