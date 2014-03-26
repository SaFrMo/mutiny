using UnityEngine;
using System.Collections;

public class GUI_RelationshipsDisplay : MonoBehaviour {

	public GUISkin skin;







	void OnGUI () {
		GUI.skin = skin;
	}
}
