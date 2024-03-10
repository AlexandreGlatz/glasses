using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWorld : MonoBehaviour
{
    public GameObject normalScene;
    public GameObject glassesScene;
    public GameObject[] trees;
    public GameObject[] footPrints;
    public GameObject[] realFp;

    private bool normalBool = true;
    private bool glassesBool = false;
    public bool isFirst = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (isFirst)
            {
                trees = GameObject.FindGameObjectsWithTag("Tree");
                footPrints = GameObject.FindGameObjectsWithTag("Footprints");
                realFp = GameObject.FindGameObjectsWithTag("RealFootprints");
                foreach (GameObject fp in footPrints)
                {
                    fp.SetActive(false);
                }
                isFirst = !isFirst;
            }
            normalBool = !normalBool;
            glassesBool = !glassesBool;

            //activates normal scene
            normalScene.SetActive(normalBool);

            foreach (GameObject tree in trees)
            {
                tree.SetActive(normalBool);
            }

            foreach (GameObject footPrint in realFp)
            {
                footPrint.SetActive(normalBool);
            }

            //activates glasses scene
            glassesScene.SetActive(glassesBool);
            foreach (GameObject fp in footPrints)
            {
                fp.SetActive(glassesBool);
            }
        }
    }

    public void resetActives()
    {
        foreach (GameObject fp in footPrints)
        {
            fp.SetActive(true);
        }
        foreach (GameObject tree in trees)
        {
            tree.SetActive(true);
        }
        foreach (GameObject rfp in realFp)
        {
            rfp.SetActive(true);
        }
        isFirst = true;
    }
}
