using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public Animator ar;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("openDoor", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void openDoor()
    {
        ar.Play("Firstdooropen");
    }
}
