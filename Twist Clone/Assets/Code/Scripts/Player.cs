using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[Tooltip("How high the player can jump to each platform.")]
	[SerializeField] private float m_JumpHeight = 1.5f;

	private bool m_CanMove = true;
	private float m_LeftAngle = 45f;
	private float m_RightAngle = -45f;
	private WorldController m_World = null;

	void Awake ()
	{
		m_World = GameObject.Find ("World").GetComponent <WorldController> ();
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

	/// Move the player to the next platform.
	void Move (float rotDirection)
	{
		Jump ();
		StartCoroutine (m_World.Rotate (Vector3.forward * rotDirection, .35f));
		m_CanMove = false;
	}

	void Jump ()
	{
		// Move the player into the air.
		transform.Translate (Vector3.up * m_JumpHeight);
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