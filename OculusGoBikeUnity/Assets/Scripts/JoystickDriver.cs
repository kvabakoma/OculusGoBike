using UnityEngine;

public class JoystickDriver : MonoBehaviour, IMotionDriver
{
	public string axisName;
	float axisValue;

	public float GetDeltaOffset()
	{
		return axisValue;
	}
	void Update()
	{
		axisValue = Input.GetAxis(axisName);
	}
}
