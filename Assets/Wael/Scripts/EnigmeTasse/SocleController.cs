using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocleController : MonoBehaviour
{
    [SerializeField] private List<GameObject> listSocles;

    private int[] listCodesSocles;
    public Dictionary<GameObject, int> codesSocles;

    public Dictionary<GameObject, bool> soclesLockedStatus;
    public GameObject objectToDisplay;
    private void Start()
    {
        //Initialize array and dictionary for socles
        listCodesSocles = new int[listSocles.Count];

        codesSocles = new Dictionary<GameObject, int>();
        soclesLockedStatus = new Dictionary<GameObject, bool>();

        foreach (GameObject socle in listSocles)
        {
            listCodesSocles[listSocles.IndexOf(socle)] = socle.GetComponent<SocleTasse>().keySocle;
            codesSocles[socle] = listCodesSocles[listSocles.IndexOf(socle)];

            soclesLockedStatus[socle] = socle.GetComponent<SocleTasse>().isLocked;
        }
    }

    public void ReceiveNewStatus(GameObject socleChanged, bool newStatus)
    {
        soclesLockedStatus[socleChanged] = newStatus;

        bool puzzleSolved = true;
        foreach(GameObject socle in listSocles)
        {
            if (soclesLockedStatus[socle])
                puzzleSolved = false;
        }
        if (puzzleSolved == true)
        {
            DisplayClue();
            return;
        }
        Debug.Log("raté :((");
    }

    private void DisplayClue()
    {
        Debug.Log("le clue est trouvé");
        objectToDisplay.SetActive(true);
    }
}
