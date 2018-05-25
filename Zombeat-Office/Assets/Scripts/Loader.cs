using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Loader : MonoBehaviour {

    public GameObject gameManager;


	// Use this for initialization
	void Awake ()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);

        /*if (counter == 0)
        {
            SceneManager.LoadScene(0);
            counter++;
        }*/
	}

	// Update is called once per frame
	void Update () {
	
       
	}
}
