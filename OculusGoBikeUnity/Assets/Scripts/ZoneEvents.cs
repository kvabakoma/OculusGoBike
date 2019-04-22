using UnityEngine;

public class ZoneEvents : MonoBehaviour
{
	public CustomEvent onEnter;
	public CustomEvent onExit;

	public GameObject triggerObject;


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == triggerObject && onEnter)
		{
			onEnter.InvokeEvents();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == triggerObject && onExit)
		{
			onExit.InvokeEvents();
		}
	}
}
