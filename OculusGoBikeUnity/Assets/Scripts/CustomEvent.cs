using UnityEngine;
using UnityEngine.Events;

public class CustomEvent : MonoBehaviour
{
	public UnityEvent customEvents;

	public void InvokeEvents()
	{
		customEvents.Invoke();
	}

	public void InvokeEventsDelayed(float delay)
	{
		Invoke("InvokeEvents", delay);
	}
}
