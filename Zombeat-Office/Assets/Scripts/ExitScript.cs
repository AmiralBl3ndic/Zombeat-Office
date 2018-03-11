using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExitScript : MonoBehaviour
{

    [HideInInspector] public bool exitAllowed = false;

    [HideInInspector] public LevelManager levelManager;




    // Use this for initialization
    void Start()
    {
        // Getting access to the level manager
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update() {}


    public void allowExit()
    {
        exitAllowed = true;
    }


    public void disallowExit()
    {
        exitAllowed = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        // TODO: check Collider other type 
        // We do not want a zombie to trigger the end of the level!


        // If exit is allowed (level completed)
        if (exitAllowed)
        {
            // Saving progression
            levelManager.save();


        }
    }
}
