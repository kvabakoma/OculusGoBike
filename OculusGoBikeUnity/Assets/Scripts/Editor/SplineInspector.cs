using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spline))]
[DrawGizmo(GizmoType.Selected|GizmoType.NotInSelectionHierarchy)]
public class SplineInspector : Editor
{
	private void OnSceneGUI()
	{
		Spline spline = target as Spline;

		for (int index = 0; index < spline.PointCount(); index++)
		{
			Transform trans = spline.GetPoint(index);
			float size = HandleUtility.GetHandleSize(trans.position);
			float handleSize = 0.1f * size;
			float pickSize = 0.1f * size;
			if (Handles.Button(trans.position, trans.rotation, handleSize, pickSize, Handles.DotCap))
			{
				Selection.activeGameObject = trans.gameObject;
			}
		}
	}
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		Spline curve = target as Spline;

		if (GUILayout.Button("Add control point."))
		{
			var point = new GameObject("Point");
			point.transform.position = Vector3.zero;
			int pointCount = curve.PointCount();
			if(pointCount > 0 )
			{
				Transform prevPoint = curve.GetPoint(pointCount - 1);
				point.transform.position = prevPoint.localPosition;
				point.transform.localRotation = prevPoint.localRotation;
			}
			point.transform.SetParent(curve.transform, false);

			Selection.activeGameObject = point;
		}

		EditorGUILayout.LabelField("Length: " + curve.Length.ToString());
	}
}
