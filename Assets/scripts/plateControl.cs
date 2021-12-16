using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateControl : MonoBehaviour
{
    public Animator ar;
    public GameObject door;
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
        if(other.tag == "ball")
        {
            ar.SetBool("doorOpen", true);
            door.GetComponent<AudioSource>().Play();
        }
    }
}
