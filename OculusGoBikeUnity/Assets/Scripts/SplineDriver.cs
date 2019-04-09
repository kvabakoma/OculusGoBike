using UnityEngine;

[RequireComponent(typeof(WheelDriver))]
public class SplineDriver : MonoBehaviour {
	public Spline spline;
	public float angularDegreesPerSecond;
	public bool applySplineRotation;

	WheelDriver wheelDriver;
	
	private float progress = 0f;

	private void Start()
	{
		wheelDriver = GetComponent<WheelDriver>();
	}

	void LateUpdate () {
		if (spline == null) {
			return;
		}
		float distance = wheelDriver.GetDeltaOffset();
		if (distance < float.Epsilon) {
			return;
		}
		float currentProgress = distance/ spline.Length;
		progress += currentProgress;
		
		progress = Mathf.Clamp(progress,0.0f,1.0f);

		SplinePoint pointData = spline.GetPointOnSpline(progress);

		gameObject.transform.position = pointData.position;
		if(applySplineRotation)
		{
			Quaternion targetRotation = Quaternion.LookRotation(pointData.direction);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularDegreesPerSecond * Time.deltaTime);
		}
	}
}
