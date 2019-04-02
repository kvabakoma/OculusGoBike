using UnityEngine;

public static class BezierCurve {
	public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01 (t);
		float oneMinusT = 1f-t;
		return oneMinusT * oneMinusT * oneMinusT * p0 + 
				3f * oneMinusT * oneMinusT * t * p1 +
				3f * oneMinusT * t * t * p2 +
				t * t * t * p3;
	}
	
	private static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		t = Mathf.Clamp01 (t);
		float oneMinusT = 1f-t;
		return 3f * oneMinusT * oneMinusT * (p1-p0) +
			6f * oneMinusT * t * (p2-p1) +
				3f * t * t * (p3-p2);
	}
	public static Vector3 GetVelocity(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		return GetFirstDerivative(p0,p1,p2,p3,t);
	}
	public static Vector3 GetDirection(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {
		return GetVelocity(p0,p1,p2,p3,t).normalized;
	}
	public static float GetLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, int interpolationSteps) {
		float length = 0f;
		Vector3 start = p0;
		for (int step = 0; step < interpolationSteps; step++) {
			float t = (float)step / (interpolationSteps-1);
			Vector3 end = GetPoint(p0, p1, p2, p3, t);
			length += Vector3.Distance(start,end);
			start = end;
		}
		return length;
	}
}