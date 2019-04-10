using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WheelDriver : MonoBehaviour
{
	public XRNode controllerNode;
	public float radius;
	public bool useAngularVelocity;
	public Axis angularVelocityAxis;
	public bool useFilter;
	public float filter = 0.9f;

	float deltaOffset = 0;
	float prevDeltaOffset = 0;

	public enum Axis
	{
		X, Y, Z
	}

	Quaternion prevCtrlRotation;

	public float GetDeltaOffset()
	{
		return deltaOffset;
	}

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
#if UNITY_EDITOR
		ctrlRotation = Quaternion.Euler(Time.time * 100000f / 360f, 0f, 0f);
#endif
		Quaternion deltaRotation = Quaternion.Inverse(prevCtrlRotation) * ctrlRotation;
		
		deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);

		if (useAngularVelocity)
		{
			Vector3 angularVelocity = GetAngularVelocity();
			if (angularVelocity[(int)angularVelocityAxis] < 0)
			{
				angle *= -1f;
			}
		}

		float newDeltaOffset = (angle / 360.0f) * 2 * Mathf.PI * radius;
		if(useFilter)
		{
			deltaOffset = (newDeltaOffset * filter) + (prevDeltaOffset * (1.0f - filter));
			prevDeltaOffset = deltaOffset;
		}else
		{
			deltaOffset = newDeltaOffset;
		}

		prevCtrlRotation = ctrlRotation;
    }
}
