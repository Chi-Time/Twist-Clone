using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformGenerator : MonoBehaviour
{
	[SerializeField] private int m_Amount = 10;
	[SerializeField] private GameObject m_PlatformPrefab = null;
	[SerializeField] private PlatformPool m_Pool = new PlatformPool();

	void Awake ()
	{
		GeneratePool ();
	}

	void GeneratePool ()
	{
		for(int i = 0; i < m_Amount; i++)
		{
			var temp = (GameObject)Instantiate (m_PlatformPrefab, transform.position, Quaternion.identity);
			temp.SetActive (false);
			temp.transform.SetParent (GameObject.Find ("World").transform);
			m_Pool.AddToPool (temp.GetComponent <Platform> ());
		}
	}
}