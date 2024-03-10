using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class PathWays : MonoBehaviour
{
    [Header("Paths")]
    public Passage FirstPassage;
    private Passage nextPassage;

    public int pathWaysAmount;

    private List<int> rightPath = new List<int>();
    private List<Transform> pots = new List<Transform>(); 

    public List<Passage> passageWay = new List<Passage>();

    [Header("Plant Pots")]
    public MeshFilter plant;
    public TMP_Text plantText;
    public List<Vector3> potCoord = new List<Vector3>();
    public List<Mesh> PotMeshes = new List<Mesh>();

    [Header("Parents")]
    public GameObject parentNormalScene;
    public GameObject parentPath;
    public GameObject parentTree;

    [Header("Switch World")]
    public SwitchWorld switchWorld;

    // Start is called before the first frame update
    void Start()
    {
        nextPassage = FirstPassage;
        GenerateRightPathway();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateRightPathway()
    {
        ///Generate passway with right track 
        ClearLists();

        //Generate right path
        for (int i = 0; i < pathWaysAmount; i++)
        {
            rightPath.Add(Random.Range(1, 5));
            print(rightPath[i]);
        }
        
        for (int i = 0; i < pathWaysAmount; i++)
        {
            Shuffle(nextPassage.leavesAmount);

            //set flower pots for four first rows
            if (i < 4)
            {
                plant.mesh = PotMeshes[rightPath[i] - 1];
                plantText.text = (i + 1).ToString();
                pots.Add(Instantiate(plant.gameObject.transform, potCoord[i], Quaternion.identity, parent: parentNormalScene.transform));
            }

            int j = 0;
            foreach (GameObject pass in nextPassage.singlePath)
            {
                //set trigger boxes
                pass.transform.position -= new Vector3(0, 0, 30);
                
                pass.tag = "WrongPath";

                //put trees next to triggers
                nextPassage.trees[nextPassage.leavesAmount[j]-1].transform.position = pass.transform.position - new Vector3(4,0,0);
                nextPassage.trees[nextPassage.leavesAmount[j]-1].transform.position += new Vector3(0,2,0);

                //set right way
                if (nextPassage.leavesAmount[j] == rightPath[i])
                {
                    pass.tag = "RightPath";
                }
                j++;
            }
            passageWay.Add(Instantiate(nextPassage, parentPath.transform));

            
        }
        FirstPassage.transform.position += new Vector3(0, 0, pathWaysAmount * 30);
    }
    // ShuffleList(toShuffle);

    void Shuffle<T>(List<T> inputList)
    {
        ///Shuffles a list
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

    void ClearLists()
    {
        ///clear all lists to reset lvl
        rightPath.Clear();

        foreach (Passage passage in passageWay)
        {
            Destroy(passage.gameObject);
        }

        foreach (Transform pot in pots)
        {
            Destroy(pot.gameObject);
        }
        passageWay.Clear();
        pots.Clear();
    }


}

