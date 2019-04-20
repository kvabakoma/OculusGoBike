using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeAnimationSpeed : MonoBehaviour
{

    Animator m_Animator;
    public string axisName;
    float axisValue;
    public float editorDeltaSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator=gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
#if UNITY_EDITOR
        axisValue = editorDeltaSpeed;
#else
		axisValue = Input.GetAxis(axisName);
        //axisValue = .4f;
#endif
        m_Animator.speed = axisValue;
    }
}
