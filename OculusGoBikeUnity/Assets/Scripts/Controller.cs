using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class Controller : AxisButton
{
	Button button = null;

	void OnTriggerEnter(Collider other)
	{
		Button b = other.GetComponent<Button>();
		if(b != null)
		{
			button = b;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(button != null && other.gameObject == button.gameObject)
		{
			button = null;
		}
	}
	void LateUpdate()
    {
		Quaternion rot = InputTracking.GetLocalRotation(XRNode.RightHand);
		transform.rotation = rot;

		if (GetAxisDown())
		{
			if (button)
			{
				button.onClick.Invoke();
			}
		}
    }
}
