using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wall : MonoBehaviour {

    public Sprite dmgSprite;
    public int hp = 4;
    public int projectileWallDamage;
    public AudioClip chopSound1;
    public AudioClip chopSound2;
    public GameObject food;
    public BoardManager boardManagerScript;
    

    private Vector2 myPosition;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Awake ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myPosition = transform.position;
	}

    float random()
    {
        return Random.Range(0, 100);
    }
	
    public void DamageWall (int loss)
    {
        SoundManager.instance.RandomizeSfx(chopSound1, chopSound2);
        //spriteRenderer.sprite = dmgSprite;
        hp -= loss;
        if (hp <= 0)
        {
            gameObject.SetActive(false);
            
            if(random() <= 40)
                Instantiate(food, myPosition, Quaternion.identity);
        }
            
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "projectile")
        {
            hp -= projectileWallDamage;
        }
        if (hp <= 0)
        {
            gameObject.SetActive(false);

            if (random() <= 10)
                Instantiate(food, myPosition, Quaternion.identity);
        }
    }
}
