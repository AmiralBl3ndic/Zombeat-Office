using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MovingObject {

    public int playerDamage;
    public int basehp;
    public int hp;
    public int projectileDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;
    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;
    public GameObject food;

    private Vector2 myPosition;
    private List<Vector3> gridPositions = new List<Vector3>();

    // Use this for initialization
    protected override void Start ()
    {
        InitialiseList();
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();

	}

    void Update()
    {

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

        //if (gameObject.activeSelf)
        //  return;
        if (gameObject == null)
            return;

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

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 2; x < 5; x++)
        {
            for (int y = 2; y < 5; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);

        return randomPosition;
    }


    float random()
    {
        return Random.Range(0, 100);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(basehp == 5)
        {
            if (other.tag == "projectile")
                {
                    hp -= projectileDamage;
                    MoveEnemy();
                }

            if (hp <= 0)
            {
                //gameObject.SetActive(false);
                
                if (random() <= 80)
                {
                    myPosition = transform.position;
                    Instantiate(food, myPosition, Quaternion.identity);
                }
                Destroy(gameObject);  
            }
        }

        if (basehp == 15)
        {
            if (other.tag == "projectile")
            {
                hp -= projectileDamage;
                MoveEnemy();
            }

            if (hp <= 0)
            {
                gameObject.SetActive(false);
                for (int i = 0; i < 5; i++)
                {
                    Vector3 randomPosition = RandomPosition();
                    Instantiate(food, randomPosition, Quaternion.identity);
                }
            }
        }

        if (basehp == 30)
        {
            if (other.tag == "projectile")
            {
                hp -= projectileDamage;
                MoveEnemy();
            }

            if (hp <= 0)
            {
                gameObject.SetActive(false);
                for (int i = 0; i < 10; i++)
                {
                    Vector3 randomPosition = RandomPosition();
                    Instantiate(food, randomPosition, Quaternion.identity);
                }
            }
        }


        if (basehp == 60)
        {
            if (other.tag == "projectile")
            {
                hp -= projectileDamage;
                MoveEnemy();
            }

            if (hp <= 0)
            {
                gameObject.SetActive(false);
                for (int i = 0; i < 15; i++)
                {
                    Vector3 randomPosition = RandomPosition();
                    Instantiate(food, randomPosition, Quaternion.identity);
                }
            }
        }

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