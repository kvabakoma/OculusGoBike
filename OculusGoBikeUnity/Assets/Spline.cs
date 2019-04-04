using UnityEngine;

[System.Serializable]
public struct SplinePoint
{
	public Vector3 position;
	public Vector3 direction;
	public float distToPoint;
	public int segmentIndex;
}

[RequireComponent(typeof(SplineEditMonitor))]
public class Spline : MonoBehaviour {
	public Color splineColor = Color.white;
	public Color pointColor = Color.white;
	public Color tangentColor = Color.white;
	public int curveSteps = 10;

	[SerializeField, HideInInspector]
	private SplinePoint[] internalPoints;

	public void UpdateOnEdit()
	{
		CalculateLength();
		InterpolateInternalPoints();

	}

	public float Length
	{
		get;
		private set;
	}

	private void OnValidate()
	{
		UpdateOnEdit();
	}

	private void Start()
	{
		CalculateLength();
	}

	public int PointCount()
	{
		return transform.childCount;
	}

	public Transform GetPoint(int index)
	{
		return transform.GetChild(index);
	}

	void InterpolateInternalPoints() {
		if(PointCount() < 2)
		{
			internalPoints = null;
			return;
		}
		int splineSteps = curveSteps * (PointCount() - 1);
		if(internalPoints == null || splineSteps != internalPoints.Length)
		{
			internalPoints = new SplinePoint[splineSteps];
		}
		for (int curveIndex = 1; curveIndex < PointCount(); curveIndex++) {
			Transform point0 = GetPoint(curveIndex - 1);
			Transform point1 = GetPoint(curveIndex);
			GetBezierPoints(point0, point1, curveIndex % 2, out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3);

			for(int step = 0; step < curveSteps; step++)
			{
				float t = (float)step / (curveSteps - 1);

				int internalPointIndex = (curveIndex-1) * curveSteps + step;
				internalPoints[internalPointIndex].position = BezierCurve.GetPoint(p0,p1,p2,p3,t);
				internalPoints[internalPointIndex].direction = BezierCurve.GetDirection(p0, p1, p2, p3, t);
				internalPoints[internalPointIndex].distToPoint = 0f;

			}
		}			

		float len = 0;
		for(int index=1; index<internalPoints.Length; index++){
			len += Vector3.Distance(internalPoints[index-1].position, internalPoints[index].position);
			internalPoints[index].distToPoint = len;
		}
	}

	public void GetBezierPoints(Transform point0, Transform point1, int reverse, out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3) {
		Vector3 vec = new Vector3(1f,0f,0f);
		if (reverse==0) {
			vec *=-1;
		}
		p0 = point0.position;
		p1 = point0.position + point0.rotation*Vector3.Scale (vec, point0.localScale);
		p2 = point1.position + point1.rotation*Vector3.Scale (vec, point1.localScale);
		p3 = point1.position;
	}

	private void CalculateLength()
	{
		Length = 0f;
		if (internalPoints == null)
		{
			return;
		}
		for (int index = 1; index < internalPoints.Length; index++)
		{
			Length += Vector3.Distance(internalPoints[index-1].position, internalPoints[index].position);
		}
	}

	private delegate Vector3 GetBezierData(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t);
	private Vector3 GetBezier(float t, GetBezierData handler) {
		Vector3 result = GetPoint(PointCount()-1).position;
		float time = Mathf.Clamp01(t);
		float targetLength = Length * time;
		float currentLength = 0f;
		for (int index = 1; index < PointCount(); index++) {
			Transform point0 = GetPoint(index-1);
			Transform point1 = GetPoint(index);
			GetBezierPoints(point0,point1, index%2, out Vector3 p0, out Vector3 p1, out Vector3 p2,out Vector3 p3);
			
			float curveLength = BezierCurve.GetLength(p0,p1,p2,p3,curveSteps);
			if (currentLength + curveLength >= targetLength) {
				float curveTime = (targetLength - currentLength) / curveLength;
				result = handler(p0,p1,p2,p3,curveTime);
				break;
			}
			currentLength += curveLength;
		}
		return result;
	}

	public SplinePoint GetPointOnSpline(float t, int startSegment) {
		SplinePoint pointData = new SplinePoint();
		bool pointDataFound = false;
		if (internalPoints != null)
		{
			float atDistance = Length * t;
			for (int index = startSegment; index < internalPoints.Length; index++)
			{
				float segLen = internalPoints[index].distToPoint - internalPoints[index - 1].distToPoint;
				float dist = internalPoints[index - 1].distToPoint;

				if (dist + segLen >= atDistance)
				{
					float segPart = (atDistance - dist) / segLen;
					SplinePoint pointA = internalPoints[index - 1];
					SplinePoint pointB = internalPoints[index];
					pointData.position = Vector3.Lerp(pointA.position, pointB.position, segPart);
					pointData.direction = Vector3.Lerp(pointA.direction, pointB.direction, segPart);
					pointData.segmentIndex = index;
					pointDataFound = true;
					break;
				}
			}
		}

		if (!pointDataFound) {
			int index = internalPoints.Length-1;
			pointData.position = internalPoints[index].position;
			pointData.direction = internalPoints[index].direction;
			pointData.segmentIndex = internalPoints.Length-1;
		}

		return pointData;
	}

	void OnDrawGizmos() {
		if(internalPoints == null)
		{
			return;
		}
		for(int index=0; index<internalPoints.Length; index++){
			Gizmos.color = pointColor;
			Gizmos.DrawSphere(internalPoints[index].position, .25f);
		}
#if UNITY_EDITOR
		for (int index=1; index < PointCount(); index ++)
		{
			Transform point0 = GetPoint(index - 1);
			Transform point1 = GetPoint(index);
			GetBezierPoints(point0, point1, index % 2, out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3);

			UnityEditor.Handles.color = tangentColor;
			UnityEditor.Handles.DrawLine(p0, p1);
			UnityEditor.Handles.DrawLine(p1, p2);
			UnityEditor.Handles.DrawLine(p2, p3);
			UnityEditor.Handles.DrawBezier(p0, p3, p1, p2, splineColor, null, 2f);
		}
#endif
	}
}
