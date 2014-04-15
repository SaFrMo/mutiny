using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class WASDMovement : MonoBehaviour {

	public string powerUpTag = "Powerup";

	public KeyCode left;
	public KeyCode right;


	// IMPLEMENTATION
	// ===================
	// Attach to an object that needs WASD to move.
	// Edit controls in Controls() below as fit.

	// rate at which to move
	public float movementRate = 0.5f;
	float movementRateOriginal;
	public float YCurrent { get; private set; }


	// where the rigidbody will move
	Vector3 newPosition;

	// MOVEMENT COMPONENT
	// ====================
	// Called for each possible control. Result is added to transform.position
	// to produce accurate movement.

	protected Vector3 SingleControl(KeyCode key, Vector3 direction) {
		if (Input.GetKey (key)) {
			return direction * movementRate;
		}
		else {
			return Vector3.zero;
		}
	}

	public void SpeedChange (float amount) {
		movementRate *= amount;
	}

	public void NormalSpeed() {
		movementRate = movementRateOriginal;
	}

	// MAIN CONTROLS
	// ====================
	// Runs all Single Controls and generates a new Vector3 based on the results,
	// then uses the rigidbody (so as to detect collisions) to move the GO to that
	// new position.

	protected void Controls() {
		newPosition = transform.position +
			//SingleControl (KeyCode.W, Vector3.up) +
			SingleControl (left, Vector3.left) +
			//SingleControl (KeyCode.S, Vector3.down) +
			SingleControl (right, Vector3.right);
		rigidbody.MovePosition (newPosition);
	}

	void OnCollisionExit() {
		// prevents momentum from registering
		rigidbody.velocity = Vector3.zero;
	}

	// ROTATION - JUST DECORATION
	// ===========================
	Vector3 lastPosition;
	void Bank () {
		if (transform.position.x != lastPosition.x) {
			transform.RotateAround (transform.position, Vector3.up, (transform.position.x > lastPosition.x ?
			                                                         -5f : 5f));
		}
		lastPosition = transform.position;
	}


	void Start () {
		YCurrent = transform.position.y;
		movementRateOriginal = movementRate;
	}

	protected void FixedUpdate () {
		Controls();
	}

	void Update () {
		//Bank();
	}
}
