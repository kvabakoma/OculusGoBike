using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineDriverPreview))]
public class SplineDriverPreviewInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		SplineDriverPreview preview = target as SplineDriverPreview;

		GUILayout.BeginHorizontal();
		if (GUILayout.Button("<<"))
		{
			preview.Progress -= preview.Step;
			preview.UpdatePreview();
		}
		if (GUILayout.Button(">>"))
		{
			preview.Progress += preview.Step;
			preview.UpdatePreview();
		}
		GUILayout.EndHorizontal();
	}
}
