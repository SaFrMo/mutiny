using UnityEngine;
using System.Collections;

public class Personality {

	public Personality (string name, int type) {
		characterName = name;
		personalityType = type;
	}

	enum PersonalityType
	{
		Reformer = 1,
		Individualist = 4
	}

	public string characterName;
	public int personalityType;
}
