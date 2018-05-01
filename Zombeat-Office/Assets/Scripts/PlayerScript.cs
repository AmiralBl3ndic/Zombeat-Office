using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    [HideInInspector] public bool actionPeriod;
    [HideInInspector] public bool hasMoved;

    private const float spriteSize = 1f;

    private int combo = 0;
    private int badCombo = 0;


    void setStatus ()
    {
        if (hasMoved)
        {
            combo = 0;
            badCombo++;
        } else if (actionPeriod)
        {
            combo++;
            badCombo = 0;
        } else
        {
            badCombo++;
        }

        hasMoved = true;
    }
	
	// Update is called once per frame
    /** @description Method used to get player actions and move the character on the screen
    */
	void Update () {

        //TODO: Add touchscreen controls

        /** ZQSD and WASD controls */
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Moving player upward");

            setStatus();                

            transform.Translate(Vector3.up * spriteSize * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Moving player to the left");

            setStatus();

            transform.Translate(Vector3.left * spriteSize * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Moving the player downwards");

            setStatus();

            transform.Translate(Vector3.down * spriteSize * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Moving the player to the right");

            setStatus();

            transform.Translate(Vector3.right * spriteSize * Time.deltaTime);
        }

        // Other player input actions
        else
        {
            /** Toggle pause */
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Toggling pause");
                //TODO: do something
            }

            /** Main menu option */
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Going to the menu");
                //TODO: do something
            }
        }
	}
}
