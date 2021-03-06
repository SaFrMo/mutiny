﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerToCaptain : Conversation {

	// IMPLEMENTATION:
	/* GetContent is the main function here. First, it resets the following values:
	 * 		playerLines: the dictionary<string, int> that contains what a player says and what index that leads to
	 * 		showContinueButton: whether or not the NPC has more to say before the player can chime in
	 * 		showPlayerLine: what the player can say to the NPC
	 * 		whereTo: -1 is a flag value that means clicking on the "next" button will simply advance the conversation by 1.
	 * 			If AllowContinue(int x) is called, -1 will be replaced by x, which will take the conversation to a different
	 * 			place than the next index.
	 * 
	 */

	// this is the string the NPC will say
	string toContent;



	// allow progression to next conversation index
	void AllowContinue () {
		showContinueButton = true;
	}

	// jump to a special index
	void AllowContinue (int where) {
		showContinueButton = true;
		whereTo = where;
	}

	void AllowPlayerLines () {
		showPlayerLine = true;
	}

	// HOW TO USE
	// 1. toContent = what the NPC will say.
	// 2a. If the player is allowed to progress to the next line without any choice, call AllowContinue() or AllowContinue(int where).
	// 2b. If the player has something to say, call AllowPlayerLines() and then create:
	// 		Dictionary<string, int> playerLines = new Dictionary<string, int>() { ... };
	// 		where each string is the player dialogue choice and each int is the corresponding index of the NPC's response
	// 3. If the NPC is to be interrupted, call Interrupt (gameObject interrupter, int lineInterrupterSays); on the relevant case.
	//		Make sure to set conversationIndex on the other character to the one you want them to start out with when you speak with them next.
	protected override void GetContent (int key, out string content, out Dictionary<string, int> playerLines) {

		interruptionOverride = false;
		playerLines = null;
		showContinueButton = false;
		showPlayerLine = false;
		whereTo = -1;

		switch (key) {

		case 0:
			toContent = "Well?";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "[Appeal to emotion] Captain, sir, he's just a child. He fixed his mistake.", 10 },
				{ "[Appeal to reason] Captain, a punishment that harsh is erratic. It'll lose the faith of the crew.", 10 }
			};
			break;

		case 10:
			toContent = "We are in the midst of a battle this very moment. Do not falter when given an order.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "[Argue tactfully] Sir, I want to support you, but this is not right.", 11 },
				{ "[Argue coldly] Sir, it is your responsibility to maintain control of this ship, and this is not the way to do so.", 11 }
			};
			break;

		case 11:
			toContent = "Do it. Now.";
			AllowPlayerLines();
			playerLines = new Dictionary<string, int>() {
				{ "Sir...", 90 }
			};
			break;

		case 90:
			toContent = "[The captain turns and walks away. Failure to obey his order will mean imprisonment, execution, or worse for you and other noncompliant hands.";
			AllowContinue();
			break;

		case 91:
			DoneTalking();
			break;


		};

		if (!showConversation)
			showPlayerLine = false;

		content = toContent;
	}
	


	
}
