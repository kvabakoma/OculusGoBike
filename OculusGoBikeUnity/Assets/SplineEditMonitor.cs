using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Spline))]
public class SplineEditMonitor : MonoBehaviour
{
	Spline spline;
	int[] childInstanceIds;
	void Start()
    {
		spline = GetComponent<Spline>();   
    }

#if UNITY_EDITOR

	void RenameChildren()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).name = "Point"+i;
		}
	}
	void TakeChildOrderSnapshot()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			childInstanceIds[i] = transform.GetChild(i).GetInstanceID();
		}
	}

	bool CheckHierarchyChanges()
	{
		if (childInstanceIds == null || childInstanceIds.Length != transform.childCount)
		{
			childInstanceIds = new int[transform.childCount];
			TakeChildOrderSnapshot();
			return true;
		}
		else
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				if (childInstanceIds[i] != transform.GetChild(i).GetInstanceID())
				{
					TakeChildOrderSnapshot();
					return true;
				}
			}
			return false;
		}
	}

	bool CheckTransformChanges()
	{
		bool hasChanges = false;
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform trans = transform.GetChild(i);
			if (trans.hasChanged)
			{
				trans.hasChanged = false;
				hasChanges = true;
			}
		}
		return hasChanges;
	}

	void Update()
    {
		bool rebuild = CheckHierarchyChanges();
		if(rebuild)
		{
			RenameChildren();
		}
		else
		{
			rebuild = CheckTransformChanges();
		}
		if(rebuild)
		{
			spline.UpdateOnEdit();
		}
	}
#endif
}
