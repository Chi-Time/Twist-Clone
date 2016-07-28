using UnityEngine;
using System.Collections;

public delegate void StatesHandler (States state);

public static class EventManager : MonoBehaviour
{
	public static event StatesHandler OnStateSwitched;

	public static void SwitchState (States state)
	{
		OnStateSwitched (state);
	}
}