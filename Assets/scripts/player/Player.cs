using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public static List<Ingredient> INVENTORY = null;

	// Player stats - all accessible without GetComponent<>()
	public static float FULL_STOMACH { get; private set; }
	public static float AWAKE { get; private set; }
	public static float FIT { get; private set; }
	public static float MINDFULNESS { get; private set; }
	public static float CONVICTION { get; private set; }
	public static float DISPOSITION { get; private set; }

	// this is a separate function in case animations, etc. are needed
	public static void ChangeStat (string whichStat, float howMuch) {
		switch (whichStat) {

		case "FULL_STOMACH":
			Player.FULL_STOMACH += howMuch;
			break;

		case "AWAKE":
			Player.AWAKE += howMuch;
			break;

		case "FIT":
			Player.FIT += howMuch;
			break;

		case "MINDFULNESS":
			Player.MINDFULNESS += howMuch;
			break;

		case "CONVICTION":
			Player.CONVICTION += howMuch;
			break;

		case "DISPOSITION":
			Player.DISPOSITION += howMuch;
			break;
		};
	}


	// DEFAULT VALUES - TODO: Change according to difficulty?
	void Start () {
		// TODO: Modify this for default inventory
		if (INVENTORY == null) {
			INVENTORY = new List<Ingredient>() {
				CRAFTING_MASTER.spuds,
				CRAFTING_MASTER.salt
			};
		}
		Player.FULL_STOMACH = 50f;
		Player.AWAKE = 50f;
		Player.FIT = 80f;
		Player.MINDFULNESS = 70f;
		Player.CONVICTION = 75f;
		Player.DISPOSITION = 70f;
	}





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
