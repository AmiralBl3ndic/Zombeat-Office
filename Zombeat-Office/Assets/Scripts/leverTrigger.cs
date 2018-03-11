using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverTrigger : MonoBehaviour {

    private GameObject exit;

    private bool triggered = false;

    public bool enablePoint = false;


	// Use this for initialization
	void Start () {
        exit = GameObject.FindGameObjectWithTag("Finish");
    }
	
	// Update is called once per frame 
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            exit.GetComponent<checkAllowed>().allowExit();
            triggered = true;
        }
    }
}
