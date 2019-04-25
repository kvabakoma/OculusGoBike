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
