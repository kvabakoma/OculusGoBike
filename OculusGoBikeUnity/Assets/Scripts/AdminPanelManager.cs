using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AdminPanelManager : MonoBehaviour
{
	public GameObject adminPanel;
	public LayerMask layer;

	public OVRInput.Button button;
	public OVRInput.Controller controller;

	Camera cam;

    void Start()
    {
		cam = GetComponent<Camera>();
    }
	
    void Update()
    {
		OVRInput.Update();

		if (OVRInput.GetDown(button, controller)) {
			if(adminPanel.activeInHierarchy == true)
			{
				adminPanel.SetActive(false);
				cam.cullingMask = LayerMask.NameToLayer("Everything");
				Time.timeScale = 1f;
			} else {
				adminPanel.SetActive(true);
				cam.cullingMask = layer;
				Time.timeScale = 0f;
			}
		}
	}
}
