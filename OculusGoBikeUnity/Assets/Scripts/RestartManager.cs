using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : AxisButton
{
	private void LateUpdate()
	{
		if(GetAxisDown())
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
