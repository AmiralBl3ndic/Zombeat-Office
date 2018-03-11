using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{

    private GameObject exit;

    private bool triggered = false;

    public bool enablePoint = false;


    // Use this for initialization
    void Start()
    {
        exit = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame 
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            exit.GetComponent<CheckAllowed>().allowExit();
            triggered = true;
        }
    }
}
