using UnityEngine;

public class JoystickDriver : MonoBehaviour, IMotionDriver
{
    public string axisName;
    public float speedMultiplier = 1f;
    public float editorDeltaSpeed = 0.1f;
    float axisValue;

    public float GetDeltaOffset()
    {
        return axisValue;
    }
    void Update()
    {
#if UNITY_EDITOR
        axisValue = editorDeltaSpeed * speedMultiplier;
#else
		axisValue = Input.GetAxis(axisName) * speedMultiplier;
        //axisValue = .4f;
#endif
    }
}