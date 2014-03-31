using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterCard : MonoBehaviour {

	public string characterName;
	public int personalityType;
	public Dictionary<string, int> relationships = new Dictionary<string, int>();
	public Texture2D portrait;
}
