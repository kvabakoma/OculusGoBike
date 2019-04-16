using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCubePos : MonoBehaviour
{

    public GameObject myCube;
    public GameObject myText;

    // Start is called before the first frame update
    void Start()
    {
        myCube = GameObject.Find("Cube");
        Debug.Log(myCube.transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        myText.GetComponent<Text>().text = myCube.transform.localPosition.z.ToString();
        Debug.Log(myCube.GetComponent<Transform>().position);
    }
}
