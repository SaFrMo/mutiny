using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SaFrMo {

	static System.Random rand = new System.Random();


	/// <summary>
	/// Move the specified GameObject in the specified direction at the specified rate. Requires a Rigidbody attached to the GameObject in question.
	/// </summary>
	/// <param name="rate">Rate.</param>
	/// <param name="direction">Direction.</param>
	/// <param name="go">Go.</param>
	public static void MoveObject (float rate, Vector3 direction, GameObject go) {
		go.rigidbody.MovePosition (go.transform.position + 
		                        direction * rate * Time.deltaTime);
	}

	/// <summary>
	/// Gets a random true/false value (boolean value).
	/// </summary>
	/// <returns><c>true</c>, if random bool was gotten, <c>false</c> otherwise.</returns>
	public static bool GetRandomBool() {
		return (rand.NextDouble() > 0.5);
	}


	/// <summary>
	/// Rotates and resizes the object. Useful for attention-grabbing things - powerups, etc. Call on the object's Update().
	/// </summary>
	/// To make this more flexible, could add Vector3 v to the signature, but seems unnecessary for now
	/// The 2f moves the sin function up the Y axis so the value doesn't come back as 0 or negative
	public static void RotateAndResize (Transform t, float rotationRate, float resizeRate) {
		t.RotateAround (t.position, t.TransformDirection (Vector3.forward), rotationRate * Time.deltaTime);
		t.localScale = Vector3.one * (Mathf.Sin (Time.time * resizeRate) + 2f);
	}

	/// <summary>
	/// Creates a solid color box - avoids need to manually create solid-color textures
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="color">Color.</param>
	public static Texture2D CreateColor (Color color) {
		Texture2D newTexture = new Texture2D (1, 1);
		newTexture.SetPixel (0, 0, color);
		newTexture.Apply();
		return newTexture;
	}

	/// <summary>
	/// Returns circle points. Think InfoAddict in Civ V.
	/// </summary>
	/// <param name="points">Points.</param>
	/// <param name="radius">Radius.</param>
	/// <param name="center">Center.</param>
	public static List<float> DrawCirclePoints(int points, float radius, float centerX, float centerY)
	{
		float slice = 2 * Mathf.PI / points;
		for (int i = 0; i < points; i++)
		{
			float angle = slice * i;
			float newX = (float)(centerX + radius * Mathf.Cos(angle));
			float newY = (float)(centerY + radius * Mathf.Sin(angle));
			return new List<float>() { newX, newY };
		}
	}
}
