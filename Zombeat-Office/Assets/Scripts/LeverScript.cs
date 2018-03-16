using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    private bool triggered = false;

    public bool enablePoint = false;


    // Use this for initialization
    void Start() { }

    // Update is called once per frame 
    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered)
        {
            GameObject.FindGameObjectWithTag("Finish").GetComponent<ExitScript>().allowExit();
            triggered = true;
        }
    }
}
