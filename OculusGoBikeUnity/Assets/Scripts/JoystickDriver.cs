using UnityEngine;

public class JoystickDriver : MonoBehaviour, IMotionDriver
{
	public string axisName;
	public float editorDeltaSpeed = 0.1f;
	float axisValue;

	public float GetDeltaOffset()
	{
		return axisValue;
	}
	void Update()
	{
#if UNITY_EDITOR
		axisValue = editorDeltaSpeed;
#else
		axisValue = Input.GetAxis(axisName);
        //axisValue = .4f;
#endif
    }
}
