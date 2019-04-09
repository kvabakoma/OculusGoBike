using UnityEngine;

[RequireComponent(typeof(Spline))]
[RequireComponent(typeof(LineRenderer))]
public class SplineDebugDraw : MonoBehaviour
{
    void Start()
    {
		Spline curve = GetComponent<Spline>();
		LineRenderer rend = GetComponent<LineRenderer>();

		int count = curve.InternalPointCount();
		rend.positionCount = count;

		if (Debug.isDebugBuild)
		{
			for (int i = 0; i < count; i++)
			{
				rend.SetPosition(i, curve.InternalPointPosition(i));
			}
		}
    }
}
