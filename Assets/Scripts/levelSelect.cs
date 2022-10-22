using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelSelect : MonoBehaviour
{
    public List<GameObject> checkpoints;

    private GameObject currentSelection;
    private int currentIndex;

    public void next()
    {
        currentSelection.SetActive(false);
        
        if (currentIndex < checkpoints.Count-1)
        {
            currentIndex += 1;
        }
        currentSelection = checkpoints[currentIndex];
        currentSelection.SetActive(true);
    }

    public void prev()
    {
        currentSelection.SetActive(false);

        if (currentIndex > 0)
        {
            currentIndex -= 1;
        }
        currentSelection = checkpoints[currentIndex];
        currentSelection.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        currentSelection = checkpoints[currentIndex];

        foreach (GameObject go in checkpoints)
        {
            go.SetActive(false);
        }
        currentSelection.SetActive(true);
    }

}
