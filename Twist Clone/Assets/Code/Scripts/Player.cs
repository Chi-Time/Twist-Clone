using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField] private float m_JumpHeight = 0.0f;
	[SerializeField] private float m_Gravity = .1f;
	[SerializeField] private WorldController m_WorldPrefab = null;

	private float m_RightAngle = -45f;
	private float m_LeftAngle = 45f;
	private bool m_CanMove = true;
	private Quaternion m_CurrentRotation = Quaternion.identity;
	private Rigidbody m_Rigidbody = null;

	void Awake ()
	{
		m_Rigidbody = GetComponent <Rigidbody> ();
	}

	void Update ()
	{
		CheckMovementInput ();
	}

	void CheckMovementInput ()
	{
		if(m_CanMove)
		{
			if(Input.GetKeyDown (KeyCode.D))
			{
				Move (m_RightAngle);
			}
			else if (Input.GetKeyDown ((KeyCode.A)))
			{
				Move (m_LeftAngle);
			}
		}
	}

	// Move the player to the next platform.
	void Move (float rotDirection)
	{
		Jump ();
		//m_WorldPrefab.transform.rotation = m_CurrentRotation;
		StartCoroutine (m_WorldPrefab.Rotate (Vector3.forward * rotDirection, .35f));
		m_CanMove = false;
	}

	void Jump ()
	{
		// Move the player into the air.
		transform.Translate (Vector3.up * m_JumpHeight);
	}

	/// Rotate's the object to the position given over the amount of time passed in.
	IEnumerator Rotate (Vector3 direction, float time)
	{
		// The amount of time that has elapsed since the coroutine began.
		float elapsedTime = 0.0f;
		// The current starting rotation of the object.
		Quaternion startingRotation = m_WorldPrefab.transform.rotation;

		// Floor the values of the starting rotation so that their is no misplaced values.
		direction.x += Mathf.Floor (startingRotation.eulerAngles.x);
		direction.y += Mathf.Floor (startingRotation.eulerAngles.y);
		direction.z += Mathf.Floor (startingRotation.eulerAngles.z);

		// Assign this new floored vector to a Quaternion.
		m_CurrentRotation = Quaternion.Euler (direction);

		// Whilst the elapsed time is less than the length of the coroutine.
		while (elapsedTime < time) 
		{
			// Increase the elapsed time.
			elapsedTime += Time.deltaTime;
			// Slerp the objects rotation between the starting rotation and the target rotation to go to over the amount of elapsed / by the length of the coroutine.
			m_WorldPrefab.transform.rotation = Quaternion.Slerp (startingRotation, m_CurrentRotation, (elapsedTime / time));
			// Wait for the end of the frame before repeating.
			yield return new WaitForEndOfFrame ();
		}

		// After this is complete, ensure that the object is now placed within the final destination.
		m_WorldPrefab.transform.rotation = Quaternion.Euler (direction);

		// End the coroutine.
		yield return 0;
	}

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.CompareTag ("Platform"))
			m_CanMove = true;
	}

	void OnCollisionExit (Collision other)
	{
		if(other.gameObject.CompareTag ("Platform"))
			m_CanMove = false;
	}
}