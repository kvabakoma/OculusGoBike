using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SplineDriver))]
public class SplineDriverPreview : MonoBehaviour
{
	const float maxProgress = 100f;
	[Range(0f, maxProgress)]
	public float Progress;

	[Range(0f,1f)]
	public float Step = 0.01f;

	Spline curve;
#if UNITY_EDITOR
	void Update()
	{
		if(curve == null)
		{
			SplineDriver driver = GetComponent<SplineDriver>();
			curve = driver.spline;
		}

		float t = Progress / maxProgress;
		SplinePoint point = curve.GetPointOnSpline(t);
		gameObject.transform.position = point.position;
	}
	public void UpdatePreview()
	{
		Update();
	}
#endif
}
