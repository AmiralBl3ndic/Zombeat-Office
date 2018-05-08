using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversPlacer : MonoBehaviour {

    [Header("Number of levers to spawn")]
    public bool useMinMax = true;
    public int minimumToSpawn = 1;
    public int maximumToSpawn = 2;

    public bool usePreciseNumber = false;
    public int leversToSpawn = 0;

    [Space(15)]
    [Header("Levers (GameObjects)")]
    [Space(5)]
    public GameObject enabledLeversHolder;
    public List<GameObject> leverPoints;


	void Start ()
    {
        if (!CheckParameters())
        {
            Debug.Log("Wrong parameters value(s), exiting the Start() method");
            return;
        }

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

    // Use this method to check the parameters' value(s)
    private bool CheckParameters()
    {
        // Number of potential levers
        if (leverPoints.Count <= 0)
        {
            Debug.LogError(string.Format("No potential levers were passed"));
            return false;
        }

        // Precise number of levers to spawn if using precise number
        if (usePreciseNumber && (leversToSpawn < 0 || leversToSpawn > leverPoints.Count))
        {
            Debug.LogError(string.Format("Cannot spawn {0} levers with {1} items passed", leversToSpawn, leverPoints.Count));
            return false;
        }

        // Minimum and maximum levers to spawn if not using precise number and using minimum and maximum
        if (!usePreciseNumber && useMinMax && (minimumToSpawn < 0 || minimumToSpawn > leverPoints.Count || maximumToSpawn < 0 || maximumToSpawn > leverPoints.Count))
        {
            Debug.Log(string.Format("Values passed for minimum and maximum levers to spawn are not correct, setting them to 1 and {0} instead", leverPoints.Count));

            minimumToSpawn = 1;
            maximumToSpawn = leverPoints.Count;
        }

        return true;
    }


    private void SetLeverPoints()
    {
        // Setting a proper number of levers
        int numberOfLevers = 0;
        if (usePreciseNumber)
            numberOfLevers = leversToSpawn;
        else if (useMinMax)
            numberOfLevers = RandIntInRange(minimumToSpawn, maximumToSpawn);
        else
            numberOfLevers = RandIntInRange(1, leverPoints.Count);


        // Activating the correct amount of levers at random
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
