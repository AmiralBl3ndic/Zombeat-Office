using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkAllowed : MonoBehaviour {

    [HideInInspector] public bool exitAllowed = false;


    public void allowExit()
    {
        exitAllowed = true;
    }


    public void disallowExit()
    {
        exitAllowed = false;
    }


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        // If exit is allowed (level completed)
        if (exitAllowed)
        {
            //TODO: implement level end
        }
    }
}
