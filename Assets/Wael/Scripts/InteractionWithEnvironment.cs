using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionWithEnvironment : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float pickUpDistance = 3.0f;
    [SerializeField] private LayerMask pickUpLayerMask;

    private Transform highlight;
    private RaycastHit hitInfo;

    private ObjectGrabbable objectGrabbed = null;
    void Update()
    {
        // Highlight outline
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Vector3 rayDirection = playerCamera.transform.forward;
        bool rayHit = Physics.Raycast(playerCamera.transform.position, rayDirection, out hitInfo, pickUpDistance, pickUpLayerMask);
        if (rayHit)
        {
            highlight = hitInfo.transform;
            if (highlight.gameObject.GetComponent<Outline>() != null)
            {
                highlight.gameObject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                Outline outline = highlight.gameObject.AddComponent<Outline>();
                outline.enabled = true;
            }
        }

        // Pickup/Drop Items
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbed == null)
            {
                // Not carrying an object, try to grab
                if (rayHit)
                {
                    if (hitInfo.transform.TryGetComponent(out objectGrabbed))
                    {
                        objectGrabbed.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                // Carrying an object, drop it
                objectGrabbed.Drop();
                objectGrabbed = null;
            }
        }

        // Keypad Key Interaction 

        if (rayHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hitInfo.transform.GetComponent<KeypadKey>() != null)
                {
                    hitInfo.transform.GetComponent<KeypadKey>().SendKey();
                }
            }
        }
    }
}



