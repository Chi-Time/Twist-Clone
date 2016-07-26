using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField] private float m_JumpHeight = 0.0f;
	[SerializeField] private float m_Gravity = 9.81f;
	[SerializeField] private GameObject m_WorldPrefab = null;

	private bool m_CanMove = true;
	private Quaternion CurrentRotation = Quaternion.identity;
	private Rigidbody m_Rigidbody = null;

	void Start ()
	{
		m_Rigidbody = GetComponent <Rigidbody> ();
	}

	void Update ()
	{
		PollInput ();
	}

	void PollInput ()
	{
		CheckMovement ();
	}

	void CheckMovement ()
	{
		if(m_CanMove)
		{
			if(Input.GetKeyDown (KeyCode.D))
			{
				m_WorldPrefab.transform.rotation = CurrentRotation;
				StartCoroutine (Rotate (new Vector3(0.0f, 0.0f, -45f), .35f));
				Jump ();
			}
			else if (Input.GetKeyDown ((KeyCode.A)))
			{
				m_WorldPrefab.transform.rotation = CurrentRotation;
				StartCoroutine (Rotate (new Vector3(0.0f, 0.0f, 45f), .35f));
				Jump ();
			}
		}
	}

	IEnumerator Rotate (Vector3 direction, float time)
	{
		m_CanMove = false;
		float elapsedTime = 0.0f;

		Quaternion startingRotation = m_WorldPrefab.transform.rotation; // have a startingRotation as well

		direction.x += Mathf.Floor (startingRotation.eulerAngles.x);
		direction.y += Mathf.Floor (startingRotation.eulerAngles.y);
		direction.z += Mathf.Floor (startingRotation.eulerAngles.z);

		CurrentRotation = Quaternion.Euler (direction);

		Quaternion targetRotation =  Quaternion.Euler (direction);

		while (elapsedTime < time) 
		{
			elapsedTime += Time.deltaTime;
			// Rotations
			m_WorldPrefab.transform.rotation = Quaternion.Slerp (startingRotation, targetRotation, (elapsedTime / time));
			yield return new WaitForEndOfFrame ();
		}

		m_WorldPrefab.transform.rotation = Quaternion.Euler (direction);

		yield return 0;
	}

	void Jump ()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y + m_Gravity * m_JumpHeight, transform.position.z);
	}

	void OnCollisionEnter (Collision other)
	{
		if(other.gameObject.CompareTag ("Platform"))
			m_CanMove = true;
	}
}