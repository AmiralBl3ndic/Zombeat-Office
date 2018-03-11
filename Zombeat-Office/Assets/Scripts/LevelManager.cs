using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

    public int levelID = 0;

    public List<GameObject> leverPoints;
    public GameObject enabledLeversHolder;
    
    public int numberOfEnemiesToSpawn;
    public List<GameObject> enemies;
    public GameObject enabledEnemiesHolder;

    //public AudioClip musicClip; // The music clip associated to that level


    // Use this for initialization
    void Start() {

        // Disabling all potential levers and then activating a few of them
        for (int i = 0; i < leverPoints.Count; i++)
            leverPoints[i].SetActive(false);
        SetLeverPoints();  // Enabling some lever points 
        GameObject.Find("PotentialLevers").SetActive(false);  // (Simple security) Disabling parent object of all potential levers
        enabledLeversHolder.SetActive(true);  // Activating levers holder


        // Disabling all potential enemies and then activating a few of them
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].SetActive(false);
        SetEnemies();
        GameObject.Find("EnemiesHolder").SetActive(false);
        enabledEnemiesHolder.SetActive(true);

    }

    // Update is called once per frame
    void Update() { }


    private int RandIntInRange(int min, int max)
    {
        return Mathf.CeilToInt(Random.Range(min, max + 1));
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


    private void SetEnemies()
    {
        // TODO: find a better way to balance the number of enemies
        if (numberOfEnemiesToSpawn > enemies.Count || numberOfEnemiesToSpawn < 0)
            numberOfEnemiesToSpawn = RandIntInRange(1, Mathf.FloorToInt(enemies.Count / 2));

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // Getting a random subscript inside the enemies List
            int randomEnemyID = RandIntInRange(0, enemies.Count - 1);

            // Adding the matching enemy to the enabledEnemiesHolder GameObject and activating it
            enemies[randomEnemyID].transform.SetParent(enabledEnemiesHoder.transform);
            enemies[randomEnemyID].SetActive(true);

            // Removing the occurence from the List so that it is not activated twice
            enemies.RemoveAt(randomEnemyID);
        }
    }

        

// Public methods

    public void Save()
    {
        /*
            Saving the player's progression
         
            We do not save if the level ID is lower than the maximum levelID reached by the player
        */

        if (PlayerPrefs.GetInt("maxLevel") > levelID)
        {
            PlayerPrefs.SetInt("maxLevel", levelID);
        }
    }
}
