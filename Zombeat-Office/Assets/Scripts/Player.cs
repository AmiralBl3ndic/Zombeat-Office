﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{

    public int wallDamage = 1;
    public int pointsPerFood = 3;

    public float restartLevelDelay = 1f;

    private Animator animator;
    private int life;


    // Use this for initialization
    protected override void Start()
    {
        animator = GetComponent<Animator>();

        life = GameManager.instance.playerLifePoints;

        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerLifePoints = life;
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        CheckIfGameOver();
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;

        CheckIfGameOver();

        GameManager.instance.playersTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "item")
        {
            life += pointsPerFood;
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("PlayerChop");
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void LoseLife(int loss)
    {
        animator.SetTrigger("PlayerHit");
        life -= loss;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (life <= 0)
            GameManager.instance.GameOver();
    }
}
