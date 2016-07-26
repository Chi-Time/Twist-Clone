using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField] private float m_JumpHeight = 0.0f;
	[SerializeField] private GameObject m_WorldPrefab = null;

	private bool m_HasMoved = false;
	private bool m_JumpPressed = false;
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
		if(!m_HasMoved)
		{
			// Move right. 
			if (Input.GetAxis ("Horizontal") > 0)
			{
				Move (new Vector3 (0, 0, -45));
				m_HasMoved = true;
				if(!m_JumpPressed)
				{
					Jump ();
					m_JumpPressed = true;
				}
			}

			// Move left.
			else if (Input.GetAxis ("Horizontal") < 0)
			{
				Move (new Vector3 (0, 0, 45));
				m_HasMoved = true;
				if(!m_JumpPressed)
				{
					Jump ();
					m_JumpPressed = true;
				}
			}
		}

		if (Input.GetAxis ("Horizontal") == 0)
		{
			m_HasMoved = false;
			m_JumpPressed = false;
		}
	}

	void Move (Vector3 direction)
	{
		var currentRotation = m_WorldPrefab.transform.rotation.eulerAngles;
		direction += currentRotation;
		m_WorldPrefab.transform.rotation = Quaternion.Euler (direction);
	}

	void Jump ()
	{
		m_Rigidbody.AddForce (new Vector3(0.0f, m_JumpHeight, 0.0f), ForceMode.Impulse);
	}
}