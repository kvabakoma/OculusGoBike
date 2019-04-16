using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
	public string axisName;

	private void Update()
	{
		if(Input.GetAxis(axisName) > 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
