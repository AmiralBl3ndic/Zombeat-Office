using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private bool canMove = false;

    [HideInInspector] public bool actionPeriod;
    [HideInInspector] public bool hasMoved;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    // To allow moves
    public void allowMove()
    {
        canMove = true;
    }

    public void disallowMove()
    {
        canMove = false;
    }
}
