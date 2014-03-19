using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Player stats - all accessible without GetComponent<>()
	public static float FULL_STOMACH { get; private set; }
	public static float AWAKE { get; private set; }
	public static float FIT { get; private set; }
	public static float MINDFULNESS { get; private set; }
	public static float CONVICTION { get; private set; }
	public static float DISPOSITION { get; private set; }








	// STAT DELTAS
	// Most of the individual gameplay is here - this is where to edit numbers

	// HUNGER
	public float fullStomachLoss;

	void FullStomachDelta () {
		// -h/turn
		FULL_STOMACH -= fullStomachLoss;
	}

	// TIREDNESS
	public float awakeLoss;
	public float awakeDeltaMax;

	void AwakeDelta () {
		// -t (rT) / turn
		AWAKE -= awakeLoss * 
	};

	/// <summary>
	/// Apply appropriate changes to all stats.
	/// </summary>
	public void EndTurn () {
		FullStomachDelta();
		AwakeDelta();
	}
}
