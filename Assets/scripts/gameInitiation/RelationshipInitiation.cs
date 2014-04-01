using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RelationshipInitiation {

	// TODO: Develop like/dislike constants
	private static float likeConstant = 1.15f;
	private static float dislikeConstant = .85f;

	private static List<int> GetLikes (int iAmA) {
		switch (iAmA) {

		// case {personality type} will get a bonus toward each personality type in the list
		case 1:
			return new List<int>() { 7 };
			break;
		case 4:
			return new List<int>() { 4 };
			break;
		case 7:
			return new List<int>() { 1 };
			break;

		default:
			return null;
			break;
		};
	}

	private static List<int> GetDislikes (int iAmA) {
		switch (iAmA) {

			// case {personality type} will get a penalty toward each personality type in the list
		case 1:
			return new List<int>() { 4 };
			break;
		case 4:
			return new List<int>() { 7 };
			break;
		case 7:
			return new List<int>() { 7 };
			break;

		default:
			return null;
			break;
		};
	}

	private static int ApplyLikesOrDislikes (int startingValue, bool likes) {
		if (likes) {
			return (int)(startingValue * likeConstant);
		}
		else {
			return (int)(startingValue * dislikeConstant);
		}
	}

	public static void INITIALIZE_MY_RELATIONSHIPS (GameObject thisCharacter) {
		int iAmA = thisCharacter.GetComponent<CharacterCard>().personalityType;
		List<int> likes = RelationshipInitiation.GetLikes (iAmA);
		List<int> dislikes = RelationshipInitiation.GetDislikes (iAmA);
		int newValue;

		foreach (GameObject otherCharacter in GAME_MANAGER.Roster) {
			// don't apply likes/dislikes toward yourself
			if (otherCharacter != thisCharacter) {

				int otherCharacterPersonalityType = otherCharacter.GetComponent<CharacterCard>().personalityType;
				int startingValue = thisCharacter.GetComponent<CharacterCard>().relationships[otherCharacter];

				// does this character inherently like or dislike the other character?
				if (likes.Contains (otherCharacterPersonalityType)) {
					newValue = RelationshipInitiation.ApplyLikesOrDislikes (startingValue, true);
				}

				// does this character inherently dislike the other character?
				else if (dislikes.Contains (otherCharacterPersonalityType)) {
					newValue = RelationshipInitiation.ApplyLikesOrDislikes (startingValue, false);
				}

				// no feelings in particular
				else {
					newValue = startingValue;
				}

				thisCharacter.GetComponent<CharacterCard>().relationships[otherCharacter] = newValue;
			}
		}
	}
}
