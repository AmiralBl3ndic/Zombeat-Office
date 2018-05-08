using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPlacer : MonoBehaviour {

	[Header("Number of ennemies to spawn")]
	public bool useMinMax = true;
    public int minimumToSpawn = 1;
    public int maximumToSpawn = 2;

    public bool usePreciseNumber = false;
    public int enemiesToSpawn = 0;

	[Space(15)]
	[Header("Enemies (Gameobjects)")]
	[Space(5)]
    public GameObject enabledEnemiesHolder;
    public List<GameObject> enemies;
    

    
	void Start ()
	{
		if (!CheckParameters ())
		{
			Debug.Log ("Wrong parameters value(s), exiting the Start() method");
			return;
		}

		// Disabling all potential enemies before enabling some of them back
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].SetActive(false);

		// Enabling some enemies
        SetEnemies();

		// (Simple security) Disabling parent object of all potential enemies
        GameObject.Find("PotentialEnemies").SetActive(false);
		// Activating enemies holders
        enabledEnemiesHolder.SetActive(true);
	}

    // Use this method to check the parameters' value(s)
	private bool CheckParameters()
	{
        // Number of potential enemies
		if (enemies.Count <= 0)
        {
			Debug.LogError (string.Format ("No potential levers were passed"));
			return false;
		}

        // Precise number of enemies to spawn if using precise number
		if (usePreciseNumber && (enemiesToSpawn < 0 || enemiesToSpawn > enemies.Count))
        {
			Debug.LogError (string.Format ("Cannot spawn {0} enemies with {1} item(s) passed", enemiesToSpawn, enemies.Count));
			return false;
		}

        // Minimum and maximum enemies to spawn if not using precise number and using minimum and maximum
        if (!usePreciseNumber && useMinMax && (minimumToSpawn < 0 || minimumToSpawn > enemies.Count || maximumToSpawn < 0 || maximumToSpawn > enemies.Count))
		{
			Debug.Log (string.Format ("Values passed for minimum and maximum enemies to spawn are not correct, setting them to 1 and {0} instead", enemies.Count));

			minimumToSpawn = 1;
			maximumToSpawn = enemies.Count;
		}

		return true;
	}


    private void SetEnemies()
    {
        // Setting a proper number of enemies
        int toSpawn = 0;
        if (usePreciseNumber)
            toSpawn = enemiesToSpawn;
        else if (useMinMax)
            toSpawn = RandIntInRange(minimumToSpawn, maximumToSpawn);
        else
            toSpawn = RandIntInRange(1, enemies.Count);

        
        // Activating the correct amount of enemies at random
        for (int i = 0; i < toSpawn; i++)
        {
            // Getting a random subscript inside the enemies List
            int randomEnemyID = RandIntInRange(0, enemies.Count - 1);

            // Adding the matching enemy to the enabledEnemiesHolder GameObject and activating it
            enemies[randomEnemyID].transform.SetParent(enabledEnemiesHolder.transform);
            enemies[randomEnemyID].SetActive(true);

            // Removing the occurence from the List so that it is not activated twice
            enemies.RemoveAt(randomEnemyID);
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
