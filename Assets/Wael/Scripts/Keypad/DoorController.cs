using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool lockedByPassword = true;

    private bool isOpen = false;
    private bool canOpen = false;
    public Animator anim;

    public LoadingScreen loadingScreen;

    public void OpenClose()
    {
        
        if (lockedByPassword)
        {
            Debug.Log("Locked by password");
            return;
        }

        // Open/Close Door
        isOpen = !isOpen;

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            canOpen = true;
            if(!lockedByPassword)
            {
                loadingScreen.LoadScene(1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            canOpen = false;
    }
}
