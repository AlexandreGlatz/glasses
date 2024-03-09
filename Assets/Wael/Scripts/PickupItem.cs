using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] private float pickUpDistance = 2.0f;
    [SerializeField] private LayerMask pickUpLayerMask;

    private ObjectGrabbable objectGrabbed = null;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbed == null)
            {
                // Not carrying an object, try to grab
                Vector3 rayDirection = playerCamera.transform.forward;
                if (Physics.Raycast(playerCamera.transform.position, rayDirection, out RaycastHit hitInfo, pickUpDistance, pickUpLayerMask))
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
    }


}
