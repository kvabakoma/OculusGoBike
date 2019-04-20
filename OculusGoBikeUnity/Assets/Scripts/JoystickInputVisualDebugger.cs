using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickInputVisualDebugger : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text= "Fire1:" + Input.GetAxis("Fire1")
        +"\n" + "Fire2:" + Input.GetAxis("Fire2")
        +"\n" + "Fire3:" + Input.GetAxis("Fire3")
        +"\n" + "Horizontal:" + Input.GetAxis("Horizontal")
        +"\n" + "Vertical:" + Input.GetAxis("Vertical")
        +"\n" + "Jump:" + Input.GetAxis("Jump")
        +"\n" + "Submit:" + Input.GetAxis("Submit")
        +"\n" + "Cancel:" + Input.GetAxis("Cancel");

        /* 
         for (int i = 0;i < 20; i++) {
             if(Input.GetAxis("joystick 1 button "+i)){
                 print("joystick 1 button "+i);
                 text.text=("joystick 1 button "+i);
             } else {
                 text.text=("no joystick btn pressed");
             }
         } */
    }
}
