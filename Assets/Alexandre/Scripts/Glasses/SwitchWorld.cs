using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorld : MonoBehaviour
{
    public GameObject normalScene;
    public GameObject glassesScene;
    public GameObject[] trees;

    private bool normalBool = true;
    private bool glassesBool = false;
    // Start is called before the first frame update
    void Start()
    {
        trees = GameObject.FindGameObjectsWithTag("Tree");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            
            normalBool = !normalBool;
            glassesBool = !glassesBool;
            normalScene.SetActive(normalBool);

            foreach (GameObject tree in trees)
            {
                tree.SetActive(normalBool);
            }

            glassesScene.SetActive(glassesBool);
        }
    }
}
