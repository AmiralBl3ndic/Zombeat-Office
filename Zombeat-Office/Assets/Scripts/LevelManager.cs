using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

    public int levelID = 0;

    public AudioClip audioclip;

    
    void Start() { }

    // Update is called once per frame
    void Update() { }


    private int RandIntInRange(int min, int max)
    {
        if (min == max)
            return min;
        else if (min < max)
            return Mathf.CeilToInt(Random.Range(min, max + 1));
        else
            return Mathf.CeilToInt(Random.Range(max, min + 1));
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
