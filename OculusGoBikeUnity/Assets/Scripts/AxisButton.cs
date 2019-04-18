using UnityEngine;

public class AxisButton : MonoBehaviour
{
	public string axisName;
	bool prevValue = false;
	bool axisDown = false;
	
    void Update()
    {
		bool value = (Input.GetAxisRaw(axisName) > 0);
		
		axisDown = (prevValue == false && value == true);

		prevValue = value;
    }

	protected bool GetAxisDown()
	{
		return axisDown;
	}
}
