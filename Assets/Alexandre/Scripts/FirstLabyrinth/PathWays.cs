using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathWays : MonoBehaviour
{
    public Passage FirstPassage;
    private Passage nextPassage;
    public int pathWaysAmount;
    private List<int> rightPath = new List<int>();

    public List<Passage> passageWay = new List<Passage>();
    // Start is called before the first frame update
    void Start()
    {
        GenerateRightPathway();
        nextPassage = FirstPassage;
        for (int i = 0; i < pathWaysAmount; i++)
        {
            int j = 0;
            foreach (GameObject pass in nextPassage.singlePath)
            {
                pass.transform.position -= new Vector3(0, 0, (i + 1) * 10);
                pass.tag = "WrongPath";
                if (j == rightPath[i])
                {
                    pass.tag = "RightPath";
                }
                j++;
            }

            passageWay.Add(nextPassage);
            Instantiate(passageWay[i]);
            print("i : "+i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GenerateRightPathway()
    {
        for (int i = 0; i < 4; i++)
        {
            rightPath.Add(Random.Range(0, 4));
            print(rightPath[i]);
        }
    }

}

