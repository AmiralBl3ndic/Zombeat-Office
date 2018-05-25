using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class projectile : MonoBehaviour
{

    public Enemy enemy;
    public int damage;
    public int wallDamage;


	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
