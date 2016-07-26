using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformGenerator
{
	[SerializeField] private int m_Amount = 10;
	[SerializeField] private GameObject m_PlatformPrefab = null;

	public void GeneratePool (PlatformPool pool)
	{
		for(int i = 0; i < m_Amount; i++)
		{
			var temp = (GameObject)MonoBehaviour.Instantiate (m_PlatformPrefab, Vector3.zero, Quaternion.identity);
			temp.SetActive (false);
			temp.transform.SetParent (GameObject.Find ("World").transform);
			pool.AddToPool (temp.GetComponent <Platform> ());
		}
	}
}