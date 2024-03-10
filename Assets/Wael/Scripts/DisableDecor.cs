using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDecor : MonoBehaviour
{
    [SerializeField] private GameObject[] Scenes;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            foreach (GameObject scene in Scenes)
            {
                if (scene.activeSelf)
                {
                    scene.SetActive(false);
                }
                else
                {
                    scene.SetActive(true);
                }
            }
        }
    }
}
