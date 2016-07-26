using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformPool
{
	[SerializeField] private List<GameObject> ActivePlatforms = new List<GameObject> ();
	[SerializeField] private List<GameObject> InactivePlatforms = new List<GameObject> ();

	public GameObject RemoveFromPool ()
	{
		if(ActivePlatforms.Count > 0)
		{
			var temp = InactivePlatforms [0];
			ActivePlatforms.Add (temp);
			InactivePlatforms.Remove (temp);

			return temp;
		}

		return null;
	}

	public void ReturnToPool (GameObject go)
	{
		ActivePlatforms.Remove (go);
		InactivePlatforms.Add (go);
	}
}