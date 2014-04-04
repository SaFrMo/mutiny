using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateOverallMood : MonoBehaviour {

	private bool moodSet = false;

	// TODO: Make this a list. I know, I know.
	private Dictionary<int, string> overallMood = new Dictionary<int, string>() {
		{ 0, "The sailing was quiet today." },
		{ 1, "It was hot, I'm sure you noticed, but otherwise unremarkable." },
		{ 2, "The captain chewed out a couple of the hands - he's grating on us all." },
		{ 3, "It looked about ready to rain today, but never started." },
		{ 4, "The cook prepared us a fine stew, but the captain never came out to eat it." },
		{ 5, "We saw an island far off the port bow, but the captain told us to carry on." },
		{ 6, "Things were quiet today." },
		{ 7, "Today was quieter than normal. A couple of the hands mentioned it." },
		{ 8, "It was oddly cold today." },
		{ 9, "One of the hands swore he saw lightning in the distance. Nothing ever came of it." },
		{ 10, "The waves were strong, though I'm sure I needn't tell you that. You felt it, eh?" },
		{ 11, "It was deadly still today. An odd thing." },
		{ 12, "The weather was calm, the mood was good - it was a good sailing day." },
		{ 13, "I saw a cloud that I swear looked like my father. That threw me off the rest of the day. My father was a real son of a bitch." },
		{ 14, "The hands wouldn't shut up about some slip-up one of them made, until one of them said that it reminded them of...you know. The cannon. They were quiet after that." },
		{ 15, "People tend to take the long way around the cannon now. No one wants to work it." },
		{ 16, "Stories all around today about our prior exploits. It was good fun until the captain shut us down - I swear he knows what you're up to and is trying to make it easier." },
		{ 17, "It was so cloudy today. Hardly a speck of sun." },
		{ 18, "The hands were quiet. Today was some anniversary for...you know. The cannon boy." },
		{ 19, "It was hot and bright today, and the captain pushed us hard." },
		{ 20, "I had a nightmare about us last night. I was thrown off all day." },
		{ 21, "The captain seemed his old self today. It was almost strange seeing him like that - then it disappeared again when he snapped at a hand and locked himself away." },
		{ 22, "The breeze today was something wonderful. I'm sorry - is that something I shouldn't say to you right now? Anyway." },
		{ 23, "More of the same today." },
		{ 24, "It's all quiet today - you'd think no one knew what we were up to." },
		{ 25, "Not one cloud today! I've seen cloudless days, but the blue today was something else." },
		{ 26, "Very quiet today." },
		{ 27, "It wasn't silent today, but it wasn't rowdy, either." },
		{ 28, "The captain's not come out of his room all day." },
		{ 29, "It's more spacious up there on the decks, but with the rest of the crew, it's easily as crowded. Just in a different sense of the word." },
		{ 30, "I'm tired. I wish we didn't have to do this." },
		{ 31, "I wanted to bring some extra food today, but the cook held my eye for a little too long. I backed down." },
		{ 32, "I hate their language. Especially since it's the only one the captain speaks." },
		{ 33, "It's a breath of fresh air to talk to you. My brain doesn't have to work like it does trying to understand them." },
		{ 34, "I want go be home, to tell you the truth." },
		{ 35, "Windy today. Good sailing, but demanding." },
		{ 36, "The crew was rowdy today. The captain hasn't shown his head, though." },
		{ 37, "We've a pool going on how long the captain will stay in his room day by day. I've won once!" },
		{ 38, "It's all quiet up there, but Lord, I miss seeing you." },
		{ 39, "Today's not much different than any other day." },
		{ 40, "It's nice to see you and talk to you. Without you up there, it's the one part of the day that feels normal." },
		{ 41, "Quiet day today. Pleasant up there. I'm sorry - I don't know if you want details like that or no." }
	
	};

	int moodToday;
	int possibleMoods;

	void Start () {
		possibleMoods = overallMood.Count - 1;
	}


	public IEnumerator OverallMoodApply() {

		// generate a random index
		int randomIndex = UnityEngine.Random.Range (0, possibleMoods);
		print (overallMood.Count);
		print (randomIndex);
		// make sure that index exists in the dictionary
		while (!overallMood.ContainsKey (randomIndex)) {
			randomIndex = UnityEngine.Random.Range (0, possibleMoods);
			yield return null;
		}

		// translate the random index to a string via the overallMood dictionary in this class
		Report.REPORT_TEXT.Add (overallMood[randomIndex]);

		// remove the random index to prevent duplicates
		overallMood.Remove (randomIndex);
	}

	public void NextTurn () {
		StartCoroutine (OverallMoodApply());
	}
}
