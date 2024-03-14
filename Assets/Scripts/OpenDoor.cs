using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void openDoor()
    {
        animator.SetBool("character_nearby", true);
        FindObjectOfType<Audio_Manager>().Play("DoorOpen");

    }

    public void closeDoor()
    {
        animator.SetBool("character_nearby", false);
        FindObjectOfType<Audio_Manager>().Play("DoorClosed");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
