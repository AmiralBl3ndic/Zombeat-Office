using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

    public int levelID = 0;

    public AudioClip musicClip; // The music clip associated to that level



    public void save()
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




	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
