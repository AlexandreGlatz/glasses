using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera1 : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
