using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
	[Tooltip("The time between platform spawns.")]
	[SerializeField] private float m_SpawnTimer = 1.0f;
	[Tooltip("The platform generator object.")]
	[SerializeField] private PlatformGenerator m_Generator = new PlatformGenerator ();
	[Tooltip("The platform pool object.")]
	[SerializeField] private PlatformPool m_Pool = new PlatformPool ();

	/// The last object to be spawned's position.
	private Transform m_LastObjectPosition = null;

	void Awake ()
	{
		m_Generator.GeneratePool (m_Pool);
	}

	void Start ()
	{
		SpawnInitialPlatforms ();
	}

	void SpawnInitialPlatforms ()
	{
		for (int i = 0; i < 4; i++)
		{
			if(i == 0)
			{
				var platform = m_Pool.RetrieveFromPool ();

				m_LastObjectPosition = m_Pool.RetrieveFromIndexPosition (0); //ActivePlatforms [0].gameObject;

				if(platform != null)
				{
					platform.transform.position = new Vector3 (0.0f, 0.0f, m_LastObjectPosition.transform.position.z + m_LastObjectPosition.transform.GetChild (0).localScale.z);
				}
			}
			else
			{
				var platform = m_Pool.RetrieveFromPool ();

				m_LastObjectPosition = m_Pool.RetrieveFromIndexPosition (i); //ActivePlatforms [i - 1].gameObject;

				if(platform != null)
				{
					platform.transform.position = new Vector3 (0.0f, 0.0f, m_LastObjectPosition.transform.position.z + m_LastObjectPosition.transform.GetChild (0).localScale.z);
				}
			}
		}

		StartCoroutine (SpawnPlatform ());
	}

	IEnumerator SpawnPlatform ()
	{
		m_LastObjectPosition = m_Pool.RetrieveLastPosition (); //ActivePlatforms [ActivePlatforms.Count - 1];

		var platform = m_Pool.RetrieveFromPool ();

		if(platform != null)
		{
			int direction = Random.Range (0, 2);

			if(direction == 0)
			{
				platform.transform.position = new Vector3 (0.0f, 0.0f, m_LastObjectPosition.transform.position.z + m_LastObjectPosition.transform.GetChild (0).localScale.z);
				platform.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, m_LastObjectPosition.transform.rotation.eulerAngles.z + 45.0f));
			}
			else if (direction == 1)
			{
				platform.transform.position = new Vector3 (0.0f, 0.0f, m_LastObjectPosition.transform.position.z + m_LastObjectPosition.transform.GetChild (0).localScale.z);
				platform.transform.rotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, m_LastObjectPosition.transform.rotation.eulerAngles.z - 45.0f));
			}
		}

		yield return new WaitForSeconds (m_SpawnTimer);
		StartCoroutine (SpawnPlatform ());
	}
}