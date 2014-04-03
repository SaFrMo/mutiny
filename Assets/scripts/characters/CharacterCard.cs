using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCard : MonoBehaviour {

	public string characterName;
	public int personalityType;
	public Dictionary<GameObject, int> relationships = new Dictionary<GameObject, int>();
	public int yourRelationship;
	public Texture2D portrait;
	public bool drawRelationshipLines = false;
	public Dictionary<string, GameObject> history = new Dictionary<string, GameObject>();
}
