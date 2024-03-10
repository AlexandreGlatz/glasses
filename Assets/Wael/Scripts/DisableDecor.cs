using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDecor : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (wall.activeSelf)
            {
                wall.SetActive(false);
            }
            else
            {
                wall.SetActive(true);
            }
        }
    }
}
