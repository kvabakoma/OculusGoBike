using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimsOnCollision : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            foreach (Transform t in gameObject.transform) {            
                Debug.Log(t.gameObject.name);
                t.gameObject.GetComponent<Animator>().SetTrigger("Play");
            }
        }
    }
}
