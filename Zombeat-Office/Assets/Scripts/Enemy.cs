using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {

    public int playerDamage;
    public int hp = 5;

    private Animator animator;
    private Transform target;
    private bool skipMove;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;



	// Use this for initialization
	protected override void Start ()
    {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        if( xDir == 0 && yDir == 0)
        {
            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
                yDir = target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = target.position.x > transform.position.x ? 1 : -1;
        }

        /*if (xDir == 1)
            animator.SetTrigger("EnemyRight");

        if (xDir == -1)
            animator.SetTrigger("EnemyLeft");

        if (yDir == 1)
            animator.SetTrigger("EnemyBack"); */

        AttemptMove<Player>(xDir, yDir);
    }

    protected override void OnCantMove <T> (T component)
    {
        Player hitPlayer = component as Player;

        animator.SetTrigger("EnemyChop");

        SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);

        hitPlayer.LoseLife(playerDamage);
    }

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected");
        int random = Random.Range(0, 3);

        if (random == 0)
        {
            if (other.tag == "InnerWall")
                MoveEnemy(1, 0);
        }
        else if (random == 1)
        {
            if (other.tag == "InnerWall")
                MoveEnemy(0, 1);
        }
        else if (random == 2)
        {
            if (other.tag == "InnerWall")
                MoveEnemy(-1, 0);
        }
        else if (random == 3)
        {
            if (other.tag == "InnerWall")
                MoveEnemy(0, -1);
        }
    }*/
}