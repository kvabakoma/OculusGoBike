using UnityEngine;

[RequireComponent(typeof(Camera))]
public class AdminPanelManager : AxisButton
{
	public GameObject adminPanel;
	public LayerMask layer;

	Camera cam;

    void Start()
    {
		cam = GetComponent<Camera>();
    }
	
    void LateUpdate()
    {
		// if (GetAxisDown()) {
		if (Input.GetAxisRaw("Oculus_CrossPlatform_PrimaryHandTrigger") != 0) {
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
