using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Metrics")]
    [SerializeField] private float MinAngle = -80;
    [SerializeField] private float MaxAngle = 80;
    [SerializeField] private float pivotHeightOffset = 1.7f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float thirdPersonColliderRadius = 0.3f;


    [Header("Linked Components")]
    [SerializeField] private Camera cam;

    [Header("Inputs")]
    [SerializeField] private string horizontalInput = "Mouse X";
    [SerializeField] private float horizontalSpeed = 2;
    [SerializeField] private string verticalInput = "Mouse Y";
    [SerializeField] private float verticalSpeed = 2;


    private float upAngle = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//Verouille le curseur dans la fenetre
        Cursor.visible = false;//Masque le curseur
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ProcessInput();
        PlaceCamera();
        ProcessThirdPerson();
    }

    void ProcessInput()
    {
        upAngle -= Input.GetAxisRaw(verticalInput) * verticalSpeed;
        upAngle = Mathf.Clamp(upAngle, MinAngle, MaxAngle);

        transform.Rotate(new Vector3(0, Input.GetAxisRaw(horizontalInput) * horizontalSpeed, 0));
    }

    void PlaceCamera()
    {
        //Défini le points de pivot
        cam.transform.position = transform.position + Vector3.up * pivotHeightOffset;

        //Oriente la caméra
        cam.transform.rotation = transform.rotation;
        cam.transform.Rotate(upAngle,0,0);
    }

    void ProcessThirdPerson()
    {
        Ray ray = new Ray(cam.transform.position, -cam.transform.forward);
        //Test les collisions
        if (Physics.SphereCast(ray, thirdPersonColliderRadius, out RaycastHit hit, distance))
        {
            //Place la caméra en fonction de la collision
            cam.transform.position -= cam.transform.forward * hit.distance;
        }else
        {
            //Place la caméra à la distance par défaut
            cam.transform.position -= cam.transform.forward * distance;
        }
    }

    //Affiche une prévisualisation de la caméra
    private void OnDrawGizmos()
    {
        //Annule la prévisualisation lorsque la simulation est lancée
        if (Application.isPlaying) return;

        Vector3 pivot = transform.position + Vector3.up * pivotHeightOffset;
        Vector3 camPos = pivot - transform.forward * distance;

        if (cam)//Place la caméra pour prévisualisation si elle est renseignée
        {
            cam.transform.position = camPos;
            cam.transform.rotation = transform.rotation;
        }

        //Affiche le point d'ancre
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pivot, 0.1f);
        Gizmos.DrawWireSphere(pivot, thirdPersonColliderRadius);

        //Affiche le collider
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(camPos, thirdPersonColliderRadius);
        
        //Affiche une ligne entre les deux
        Gizmos.DrawLine(pivot + transform.right * thirdPersonColliderRadius, camPos + transform.right * thirdPersonColliderRadius);
        Gizmos.DrawLine(pivot - transform.right * thirdPersonColliderRadius, camPos - transform.right * thirdPersonColliderRadius);
        Gizmos.DrawLine(pivot + transform.up * thirdPersonColliderRadius, camPos + transform.up * thirdPersonColliderRadius);
        Gizmos.DrawLine(pivot - transform.up * thirdPersonColliderRadius, camPos - transform.up * thirdPersonColliderRadius);
    }
}
