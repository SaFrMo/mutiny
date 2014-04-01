using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RelationshipInitiation {

	// TODO: Develop like/dislike constants
	private static float likeConstant = 1.15f;
	private static float dislikeConstant = .85f;

	// TODO: Starting lowbounds and highbounds
	private static int LOW_BOUND = 40;
	private static int HIGH_BOUND = 70;

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
			return new List<int>();
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
			return new List<int>();
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
				// TODO: Update this starting value so that it reflects mutual relationships - too random right now
				int startingValue = UnityEngine.Random.Range (LOW_BOUND, HIGH_BOUND);

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

				// apply the change
				thisCharacter.GetComponent<CharacterCard>().relationships[otherCharacter] = newValue;

				// testing purposes
				MonoBehaviour.print (string.Format("{0} to {1}: {2}", thisCharacter.name, otherCharacter.name, thisCharacter.GetComponent<CharacterCard>().relationships[otherCharacter]));
			}
		}
	}
}
