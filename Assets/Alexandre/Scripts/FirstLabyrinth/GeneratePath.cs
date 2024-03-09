using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePath : MonoBehaviour
{
    private List<int> rightPath = new List<int>();
    public List<List<PathWays>> Paths = new List<List<PathWays>>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateRightPathway();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRightPathway()
    {
        for (int i = 0; i < 4; i++) 
        {
            rightPath.Add(Random.Range(1,5));
            print(rightPath[i]);
        }
    }
}
