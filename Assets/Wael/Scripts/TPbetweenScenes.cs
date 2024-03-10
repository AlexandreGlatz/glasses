using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPbetweenScenes : MonoBehaviour
{
    [SerializeField] private Transform startPos1;
    [SerializeField] private Transform startPos2;

    private Vector3 difference;
    private bool inFirstScene = true;
    [SerializeField] private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        difference = startPos2.position - startPos1.position;
    }
    private void Start()
    {
        rb.MovePosition(startPos1.position);
    }

    private void Update()
    {
       if (Input.GetKeyDown(KeyCode.V))
        {
            if (inFirstScene)
            {
                rb.MovePosition(transform.position + difference);
                inFirstScene = !inFirstScene;
            }
            else
            {
                // Player is in second scene -> back to first scene
                rb.MovePosition(transform.position - difference);
                inFirstScene = !inFirstScene;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + difference);
    }
}
