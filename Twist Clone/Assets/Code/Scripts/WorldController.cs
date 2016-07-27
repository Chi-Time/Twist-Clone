using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour
{
	/// Rotate's the object to the position given over the amount of time passed in.
	public IEnumerator Rotate (Vector3 direction, float time)
	{
		// The amount of time that has elapsed since the coroutine began.
		float elapsedTime = 0.0f;
		// The current starting rotation of the object.
		Quaternion startingRotation = transform.rotation;

		// Floor the values of the starting rotation so that their is no misplaced values.
		direction.x += Mathf.Floor (startingRotation.eulerAngles.x);
		direction.y += Mathf.Floor (startingRotation.eulerAngles.y);
		direction.z += Mathf.Floor (startingRotation.eulerAngles.z);

		// Assign this new floored vector to a Quaternion.
		Quaternion targetRotation = Quaternion.Euler (direction);

		// Whilst the elapsed time is less than the length of the coroutine.
		while (elapsedTime < time) 
		{
			// Increase the elapsed time.
			elapsedTime += Time.deltaTime;
			// Slerp the objects rotation between the starting rotation 
			// and the target rotation to go to over the amount of elapsed / by the length of the coroutine.
			transform.rotation = Quaternion.Slerp (startingRotation, targetRotation, (elapsedTime / time));
			// Wait for the end of the frame before repeating.
			yield return new WaitForEndOfFrame ();
		}

		// After this is complete, ensure that the object is now placed within the final destination.
		transform.rotation = Quaternion.Euler (direction);

		// End the coroutine.
		yield return 0;
	}
}