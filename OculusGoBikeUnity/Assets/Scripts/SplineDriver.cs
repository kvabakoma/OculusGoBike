using UnityEngine;

public class SplineDriver : MonoBehaviour {
	public Spline spline;
	public float angularDegreesPerSecond;
	public bool applySplineRotation;
	
	public float progress = 0f;

	IMotionDriver motionDriver;
	void Start()
	{
		motionDriver = GetComponent<IMotionDriver>();
	}

	void LateUpdate () {
		if (spline == null) {
			return;
		}
		float distance = motionDriver.GetDeltaOffset();
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
