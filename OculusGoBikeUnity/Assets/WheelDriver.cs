using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WheelDriver : MonoBehaviour
{
	public XRNode controllerNode;
	public float radius;
	public Axis angularVelocityAxis;

	public enum Axis
	{
		X, Y, Z
	}

	Quaternion prevCtrlRotation;

	void Start()
	{
		prevCtrlRotation = InputTracking.GetLocalRotation(controllerNode);
	}

	Vector3 GetAngularVelocity()
	{
		List<XRNodeState> nodes = new List<XRNodeState>();
		InputTracking.GetNodeStates(nodes);
		foreach (XRNodeState ns in nodes)
		{
			if (ns.nodeType == controllerNode)
			{
				ns.TryGetAngularVelocity(out Vector3 angularVelocity);
				return angularVelocity;
			}
		}
		return Vector3.zero;
	}

    void Update()
    {
		Quaternion ctrlRotation = InputTracking.GetLocalRotation(controllerNode);
		Quaternion deltaRotation = Quaternion.Inverse(prevCtrlRotation) * ctrlRotation;
		
		deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);


		Vector3 angularVelocity = GetAngularVelocity();
		if (angularVelocity[(int)angularVelocityAxis] < 0)
		{
			angle *= -1f;
		}
		
		float deltaOffset = (angle / 360.0f) * 2 * Mathf.PI * radius;
		transform.position += new Vector3(deltaOffset, 0f);


		prevCtrlRotation = ctrlRotation;
    }
}
