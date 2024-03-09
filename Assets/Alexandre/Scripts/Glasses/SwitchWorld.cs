using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorld : MonoBehaviour
{
    public GameObject normalScene;
    public GameObject glassesScene;

    private bool normalBool = true;
    private bool glassesBool = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            normalBool = !normalBool;
            glassesBool = !glassesBool;
            normalScene.SetActive(normalBool);
            glassesScene.SetActive(glassesBool);
        }
    }
}
