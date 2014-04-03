using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateOverallMood : MonoBehaviour {

	private Dictionary<int, string> overallMood = new Dictionary<int, string>() {
		{ 0, "Nothing unusual to report today." },
		{ 1, "Today was friggin' great!" }
	};

	int moodToday;

	public void OverallMoodApply() {

		// generate a random index
		int randomIndex = UnityEngine.Random.Range (0, overallMood.Count);
		// make sure that index exists in the dictionary
		while (!overallMood.ContainsKey (randomIndex)) {
			randomIndex = UnityEngine.Random.Range (0, overallMood.Count);
		}

		// translate the random index to a string via the overallMood dictionary in this class
		Report.REPORT_TEXT.Add (overallMood[randomIndex]);

		// remove the random index to prevent duplicates
		//overallMood.Remove (randomIndex);
	}

	public void NextTurn () {
		OverallMoodApply();
	}
}
