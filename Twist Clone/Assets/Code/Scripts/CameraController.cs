using UnityEngine;
using System.Collections;

[AddComponentMenu("Prototype/Camera Movement")]
public class CameraController : MonoBehaviour
{
	/// The target object to follow.
	[Tooltip("The target object to follow.")]
	public Transform target;
	/// The distance to keep from the target.
	[Tooltip("The distance to keep from the target.")]
	public float distance = 3.0f;
	/// How high we are from the target.
	[Tooltip("How high we are from the target.")]
	public float height = 3.0f;
	/// How fast we rotate to keep focus.
	[Tooltip("How fast we rotate to keep focus.")]
	public float damping = 5.0f;
	/// Are we using smooth rotation?
	[Tooltip("Are we using smooth rotation?")]
	public bool smoothRotation = true;
	/// Should we follow behind the target.
	[Tooltip("Should we follow behind the target.")]
	public bool followBehind = true;
	/// How fast we rotate.
	[Tooltip("How fast we rotate.")]
	public float rotationDamping = 10.0f;

	void Start ()
	{
		// Assign target reference.
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void LateUpdate () 
	{
		// Is there a target to lock on to.
		if(target != null)
		{
			Vector3 wantedPosition;

			if(followBehind)
			{
				wantedPosition = target.TransformPoint (0, height, -distance);
			}
			else
			{
				wantedPosition = target.TransformPoint (0, height, distance);
			}

			transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);

			if (smoothRotation) 
			{
				Quaternion wantedRotation = Quaternion.LookRotation (target.position - transform.position, target.up);

				transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
			}
			else 
			{
				transform.LookAt (target, target.up);
			}
		}
		// Is there no target?
		else 
		{
			// Keep looking for the target and re-assign it when found.
			target = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}
}