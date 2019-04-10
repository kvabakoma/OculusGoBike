using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelDriver))]
public class WheelDriverInspector : Editor
{

	public override void OnInspectorGUI()
	{
		WheelDriver driver = target as WheelDriver;
		driver.controllerNode = (UnityEngine.XR.XRNode)EditorGUILayout.EnumPopup("Controller Node", driver.controllerNode);
		driver.radius = EditorGUILayout.FloatField("Radius", driver.radius);

		driver.useAngularVelocity = EditorGUILayout.Toggle("Use Angular Velocity", driver.useAngularVelocity);
		if (driver.useAngularVelocity)
		{
			driver.angularVelocityAxis = (WheelDriver.Axis)EditorGUILayout.EnumPopup("Angular Velocity Axis", driver.angularVelocityAxis);
		}

		driver.useFilter = EditorGUILayout.Toggle("Use Filter", driver.useFilter);
		if (driver.useFilter)
		{
			driver.filter = EditorGUILayout.FloatField("Filter", driver.filter);
			driver.filter = Mathf.Clamp01(driver.filter);
		}
	}
}
