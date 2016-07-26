using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	[Tooltip("How fast the platform should move.")]
	[SerializeField] private float m_Speed = 5.0f;

	/// Reference to the platform pool class.
	private PlatformPool m_Pool = null;

	/// Assigns the references for this class.
	public void AssignReferences (PlatformPool pool)
	{
		m_Pool = pool;
	}

	void Update ()
	{
		Move ();
		CheckPosition ();
	}
		
	/// Moves the platform towards the screen.
	void Move ()
	{
		transform.Translate (Vector3.back * m_Speed * Time.deltaTime);
	}

	/// Checks the platforms current position.
	void CheckPosition ()
	{
		if(m_Pool != null)
		{
			if (transform.position.z <= -10f)
				m_Pool.ReturnToPool (this);
		}
	}
}