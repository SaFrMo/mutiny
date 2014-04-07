using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Conversation : MonoBehaviour {

	// THE ENORMOUS AND TERRIFYING CONVERSATION-HAVER
	// ================================================
	// But it works great!

	// USE: Create a class based on GenericConversationTemplate, which is a child of this class. Follow instructions from there.
	// NOTE: You shouldn't need to attach <CONVERSATION>, this script, anywhere.

	// will display conversation text
	public bool showConversation { get; private set; }

	// these need to be overridden in children classes
	protected int conversationIndex = 0;


	// Content information
	
	protected virtual void GetContent (int key, out string content, out Dictionary<string, int> playerLines) {//out string[] playerLines) {
		content = "It's more than a name, it's an attitude.";
		playerLines = null;
	}

	// continue text
	protected bool showContinueButton = false;

	// player's dialogue
	public bool showPlayerLine = false;

	protected int whereTo = -1;

	// the conversation window itself, where all the text is displayed
	void ConversationWindow () {
		string content;
		Dictionary<string, int> playerLinesArray;
		GetContent (conversationIndex, out content, out playerLinesArray);

		// PLAYER LINES

		// side offshoot containing dialogue choices

	}
		/* reference code
				foreach (KeyValuePair<string, int> x in playerLinesArray) {
					if (GUILayout.Button (x.Key)) {
						Advance (x.Value);
					}
				}
				*/

	// rough "advance one"
	void Advance () {
		conversationIndex++;
	}

	protected void Advance (int where) {
		conversationIndex = where;
	}

	protected int conversationIndexCopy;


}
