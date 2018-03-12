using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversPlacer : MonoBehaviour {

    public List<GameObject> leverPoints;
    public GameObject enabledLeversHolder;


	void Start () {
        // Disabling all potential levers and then activating a few of them
        for (int i = 0; i < leverPoints.Count; i++)
            leverPoints[i].SetActive(false);

        // Enabling some lever points 
        SetLeverPoints();

        // (Simple security) Disabling parent object of all potential levers
        GameObject.Find("PotentialLevers").SetActive(false);
        // Activating levers holder
        enabledLeversHolder.SetActive(true);
    }

    private void SetLeverPoints()
    {
        int numberOfLevers = RandIntInRange(1, leverPoints.Count);

        for (int i = 0; i < numberOfLevers; i++)
        {
            // Getting a random subscript inside the leverPoints List
            int randomLeverID = RandIntInRange(0, leverPoints.Count - 1);

            // Adding the matching lever to the enabledLeversHolder GameObject and activating it
            leverPoints[randomLeverID].transform.SetParent(enabledLeversHolder.transform);
            leverPoints[randomLeverID].SetActive(true);

            // Removing the occurence from the List so that it is not activated twice
            leverPoints.RemoveAt(randomLeverID);
        }
    }


    private int RandIntInRange(int min, int max)
    {
        if (min == max)
            return min;
        else if (min < max)
            return Mathf.CeilToInt(Random.Range(min, max + 1));
        else
            return Mathf.CeilToInt(Random.Range(max, min + 1));
    }
}
