using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WheelDriver))]
public class WheelDriverInspector : Editor
{

	public override void OnInspectorGUI()
	{
		WheelDriver driver = target as WheelDriver;
		SerializedObject driverObj = new SerializedObject(driver);
		driver.controllerNode = (UnityEngine.XR.XRNode)EditorGUILayout.EnumPopup("Controller Node", driver.controllerNode);
		driver.radius = EditorGUILayout.FloatField("Radius", driver.radius);
		driver.useAngularVelocity = EditorGUILayout.Toggle("Use Angular Velocity", driver.useAngularVelocity);

		if(driver.useAngularVelocity)
		{
			EditorGUILayout.PropertyField(driverObj.FindProperty("angularVelocityAxis"));
		}
	}
}
