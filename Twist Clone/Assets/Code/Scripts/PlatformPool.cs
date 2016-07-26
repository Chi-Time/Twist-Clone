using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformPool
{
	[SerializeField] private List<Platform> ActivePlatforms = new List<Platform> ();
	[SerializeField] private List<Platform> InactivePlatforms = new List<Platform> ();

	public void AddToPool (Platform platform)
	{
		platform.AssignReferences (this);
		InactivePlatforms.Add (platform);
	}

	public GameObject RemoveFromPool ()
	{
		if(ActivePlatforms.Count > 0)
		{
			var temp = InactivePlatforms [0];
			ActivePlatforms.Add (temp);
			InactivePlatforms.Remove (temp);
			temp.gameObject.SetActive (true);

			return temp.gameObject;
		}

		return null;
	}

	public void ReturnToPool (Platform platform)
	{
		ActivePlatforms.Remove (platform);
		InactivePlatforms.Add (platform);
		platform.gameObject.SetActive (false);
	}
}