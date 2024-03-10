using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocleTasse : MonoBehaviour
{
    private GameObject mugPlaced = null;

    public int keySocle;
    public bool isLocked = true;
    [SerializeField] private Transform placeholder;

    public SocleController socleController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mug") && mugPlaced == null)
        {
            mugPlaced = other.gameObject;

            mugPlaced.GetComponent<ObjectGrabbable>().PlaceObjectOnSocle(placeholder);
            if (mugPlaced.GetComponent<CodeTasse>().keyTasse == keySocle)
            {
                isLocked = false;
            }
            else
            {
                isLocked = true;
            }

            SendStatus(isLocked);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == mugPlaced)
        {
            mugPlaced = null;
            isLocked = true;
        }
    }

    private void SendStatus(bool status)
    {
        socleController.ReceiveNewStatus(this.gameObject, status);
    }
}
