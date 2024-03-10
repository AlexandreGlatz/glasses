using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    private Transform objectGrabPointTransform;

    [SerializeField] private Transform basMesh;
    private Vector3 tailleMesh;

    public bool isOnSocle {  get; private set; } 
    private void Awake()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        if (basMesh != null)
        {
            tailleMesh = transform.position - basMesh.position;
        }
        isOnSocle = false;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidBody.useGravity = false;

        objectRigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        objectRigidBody.drag = 5f;
        isOnSocle = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidBody.useGravity = true;
        objectRigidBody.constraints = RigidbodyConstraints.None;
        objectRigidBody.drag = 0f;

    }
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPos = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidBody.MovePosition(newPos);

            Quaternion rot = Quaternion.Lerp(transform.rotation, objectGrabPointTransform.rotation, Time.deltaTime * lerpSpeed);
            objectRigidBody.MoveRotation(rot);
        }
    }

    public void PlaceObjectOnSocle(Transform placeholderSocle)
    {
        Drop();
        objectRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        objectRigidBody.useGravity = false;
        isOnSocle = true;
        transform.position = placeholderSocle.position + tailleMesh ;
        transform.rotation = placeholderSocle.rotation;
    }
}
