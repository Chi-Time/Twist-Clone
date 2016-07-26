using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour
{
	private PlatformPool m_Pool = null;

	public void AssignReferences (PlatformPool pool)
	{
		m_Pool = pool;
	}

	void OnDisable ()
	{
		m_Pool.ReturnToPool (this);
	}
}