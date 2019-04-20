using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
	public OVRInput.Button button;
	public OVRInput.Controller controller;

	private void LateUpdate()
	{
		if (OVRInput.GetDown(button, controller))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
