using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlatformPool
{
	[SerializeField] private List<Platform> ActivePlatforms = new List<Platform> ();
	[SerializeField] private List<Platform> InactivePlatforms = new List<Platform> ();

	/// Adds a platform to the pool and set's it up ready for use.
	public void AddToPool (Platform platform)
	{
		platform.AssignReferences (this);
		InactivePlatforms.Add (platform);
	}

	/// Retrieves an object from the pool and activates it for use.
	public GameObject RetrieveFromPool ()
	{
		// Ensure that there are objects in the pool before retrieval.
		if(InactivePlatforms.Count > 0)
		{
			// Get the first platform from the pool, this ensures that we do not get an out of bounds exception.
			var platform = InactivePlatforms [0];
			// Make sure that it's active.
			platform.gameObject.SetActive (true);

			// Add this platform to the now active objects list.
			ActivePlatforms.Add (platform);
			// And remove it from the inactive objects list.
			InactivePlatforms.Remove (platform);

			// Return the object to the caller.
			return platform.gameObject;
		}

		// The pool was empty, no object could be retrieved.
		return null;
	}

	/// Returns the specified platform back to the pool and de-activates it.
	public void ReturnToPool (Platform platform)
	{
		// Remove the returned object from the active pool.
		ActivePlatforms.Remove (platform);
		// Add it back to the inactive pool.
		InactivePlatforms.Add (platform);
		// Ensure that it is made inactive.
		platform.gameObject.SetActive (false);
	}

	/// Retrieves the provided index from the pool by - 1 position. If 0, it retrieves the 0 index's position.
	public Transform RetrieveFromIndexPosition (int index)
	{
		if(index == 0)
		{
			// If the index is zero we can not subtract from it.
			return ActivePlatforms [index].transform;
		}
		else
		{
			// The index is not zero and so we can subtract from it.
			return ActivePlatforms [index - 1].transform;
		}
	}

	/// Retrieves the last known active object's position from the pool.
	public Transform RetrieveLastPosition ()
	{
		return ActivePlatforms [ActivePlatforms.Count - 1].transform;
	}
}