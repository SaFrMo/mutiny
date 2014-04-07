using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NEXT_TURN : MonoBehaviour {
	
	static GenerateOverallMood g;
	static RandomEvents r;


	void Start () {
		g = this.GetComponent<GenerateOverallMood>();
		r = this.GetComponent<RandomEvents>();

	}

	public static void GO () {
		g.NextTurn();
		r.NextTurn();
	}
}
