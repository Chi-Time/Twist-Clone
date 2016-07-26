using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformGenerator
{
	[Tooltip("The amount of platforms to spawn in the pool.")]
	[SerializeField] private int m_PoolSize = 10;
	[Tooltip("The platform prefab to spawn.")]
	[SerializeField] private GameObject m_PlatformPrefab = null;

	/// Generates all the pool objects, names them, de-activates them and adds them to the pool.
	public void GeneratePool (PlatformPool pool)
	{
		for(int i = 0; i < m_PoolSize; i++)
		{
			var temp = (GameObject)MonoBehaviour.Instantiate (m_PlatformPrefab, Vector3.zero, Quaternion.identity);

			temp.SetActive (false);
			temp.name = "Handler: " + i;
			temp.transform.SetParent (GameObject.Find ("World").transform);

			var platform = temp.GetComponent <Platform> ();
			pool.AddToPool (platform);
		}
	}
}