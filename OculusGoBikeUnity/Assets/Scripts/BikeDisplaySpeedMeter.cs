using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BikeDisplaySpeedMeter : MonoBehaviour
{
    public GameObject Chest;
    public SplineDriver MySplineDriver;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(MySplineDriver.progress);
        //Debug.Log(Bike.transform.position);
        //float dist = Vector3.Distance(Chest.transform.position, Bike.transform.position);
        //Debug.Log(transform.position + " | " + Chest.transform.position + " | " + dist);    
        Debug.Log(Mathf.Floor(Time.timeSinceLevelLoad) % 60);
        string mins = "00";
        if (Time.timeSinceLevelLoad / 60 > 1)
        {
            mins = Mathf.Floor(Time.timeSinceLevelLoad / 60).ToString();
        }
        string secs = (Mathf.Floor(Time.timeSinceLevelLoad) % 60).ToString();
        float dist = 1035 * MySplineDriver.progress;
        text.text =
            mins + ":" + secs + "s\n"+
            Mathf.Floor(dist).ToString() + "m";
    }
}
