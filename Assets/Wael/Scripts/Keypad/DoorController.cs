using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool lockedByPassword = true;

    private bool isOpen = false;
    private bool canOpen = false;
    public Animator anim;

    public void OpenClose()
    {
        
        if (lockedByPassword)
        {
            Debug.Log("Locked by password");
            return;
        }

        // Open/Close Door
        anim.SetTrigger("Door");
        isOpen = !isOpen;

    }

    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenClose();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canOpen = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canOpen = false;
    }
}
