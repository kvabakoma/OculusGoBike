using UnityEngine;

public class ZoneEvents : MonoBehaviour
{
	public CustomEvent onEnter;
	public CustomEvent onExit;

	void OnTriggerEnter(Collider other)
	{
		if(onEnter)
		{
			onEnter.InvokeEvents();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (onExit)
		{
			onExit.InvokeEvents();
		}
	}
}
