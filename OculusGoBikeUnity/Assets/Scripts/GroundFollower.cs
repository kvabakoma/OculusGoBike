using UnityEngine;

[ExecuteInEditMode]
public class GroundFollower : MonoBehaviour
{
	public Transform wheelFront;
	public Transform wheelRear;
	public float rayStartOffset = 10;
	public float rayLength = 100;
	public LayerMask mask;

	bool GetRaycast(Vector3 position, out Vector3 hit)
	{
		bool result = Physics.Raycast(new Ray(position + rayStartOffset * Vector3.up, Vector3.down), out RaycastHit hitInfo, rayLength, mask);
		hit = hitInfo.point;
		return result;
	}

	void LateUpdate()
    {
		bool hitFront = GetRaycast(wheelFront.position, out Vector3 pointFront);
		bool hitRear = GetRaycast(wheelRear.position, out Vector3 pointRear);

		if(hitFront && hitRear)
		{
			Vector3 dir = pointFront - pointRear;

			transform.LookAt(transform.position + dir, Vector3.up);

			Vector3 offset = pointRear - wheelRear.position;
			transform.position += offset;
		}
	}
}
