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
	// =============
	public float fullStomachLoss;

	void FullStomachDelta () {
		// -h
		FULL_STOMACH -= fullStomachLoss;
	}


	
	// TIREDNESS
	// ==============
	public float awakeLoss;

	void AwakeDelta () {
		// -t(rT)
		AWAKE -= (awakeLoss); //TODO: rT
	}



	// FITNESS
	// ============
	public float fitLoss;

	void FitnessDelta () {
		// -f(HT)
		FIT -= (fitLoss); //TODO: HT
	}



	// MINDFULNESS
	// ============
	public float mindfulnessLoss;

	void MindfulnessDelta () {
		// -mF(HT)(F)
		MINDFULNESS -= mindfulnessLoss; //TODO: HT, F
	}



	// CONVICTION
	// ============
	public float convictionLoss;

	void ConvictionDelta () {
		// +c(HT)
		CONVICTION += convictionLoss; //TODO: HT
	}



	// DISPOSITION
	// ============
	public float dispositionLoss;

	void DispositionDelta () {
		// -d(HT)(M)
		DISPOSITION -= dispositionLoss; //TODO: HT, M
	}




	/// <summary>
	/// Apply appropriate changes to all stats.
	/// </summary>
	public void EndTurn () {
		FullStomachDelta();
		AwakeDelta();
		FitnessDelta();
		MindfulnessDelta();
		ConvictionDelta();
		DispositionDelta();
	}
}
