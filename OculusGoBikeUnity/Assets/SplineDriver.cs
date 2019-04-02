using UnityEngine;

[RequireComponent(typeof(WheelDriver))]
public class SplineDriver : MonoBehaviour {
	public Spline spline;
	public float angularDegreesPerSecond;
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

		SplinePoint pointData = spline.GetPointOnSpline(progress, 1);

		gameObject.transform.position = pointData.position;
		Quaternion targetRotation = Quaternion.LookRotation(pointData.direction);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularDegreesPerSecond * Time.deltaTime);
	}
}
