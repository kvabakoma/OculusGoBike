using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public OVRInput.Button button;
	public OVRInput.Controller controller;

	Button buttonComponent = null;

	void OnTriggerEnter(Collider other)
	{
		Button b = other.GetComponent<Button>();
		if(b != null)
		{
			buttonComponent = b;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(buttonComponent != null && other.gameObject == buttonComponent.gameObject)
		{
			buttonComponent = null;
		}
	}
	void LateUpdate()
    {
		Quaternion rot = InputTracking.GetLocalRotation(XRNode.RightHand);
		transform.rotation = rot;

		if (OVRInput.GetDown(button, controller))
		{
			if (buttonComponent)
			{
				buttonComponent.onClick.Invoke();
			}
		}
    }
}
