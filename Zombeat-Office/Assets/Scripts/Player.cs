using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{

    public int wallDamage = 1;
    public int pointsPerFood = 3;
    public float restartLevelDelay = 1f;
    public Text lifeText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip gameOverSound;

    private Animator animator;
    private int life;
    private Vector2 touchOrigin = -Vector2.one;


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

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");

        if (horizontal != 0)
            vertical = 0;
#else

        if(Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if(myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if ( myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0 )
            {
                Vector2 touchEnd = myTouch.position;
                float x = touchEnd.x - touchOrigin.x;
                float y = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                    horizontal = x > 0 ? 1 : -1;
                else
                    vertical = y > 0 ? 1 : -1;
            }
        }

#endif
        if (horizontal == 1)
            animator.SetTrigger("PlayerRight");
        else
        if (horizontal == -1)
            animator.SetTrigger("PlayerLeft");
        else
        if (vertical == 1)
            animator.SetTrigger("PlayerBack");
        else
        if (vertical == -1)
            animator.SetTrigger("PlayerFront");
        
        if (horizontal != 0 || vertical != 0)
            AttemptMove<Wall>(horizontal, vertical);

    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        lifeText.text = "HP: " + life;
        base.AttemptMove<T>(xDir, yDir);

        RaycastHit2D hit;
        if (Move(xDir,yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }

        GameManager.instance.playersTurn = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            life += pointsPerFood;
            lifeText.text = "+" + pointsPerFood + " HP: " + life;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
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
        lifeText.text = "-" + loss + " HP: " + life;
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if (life <= 0)
        {
            SoundManager.instance.PlaySingle(gameOverSound);
            SoundManager.instance.musicSource.Stop();

            GameManager.instance.GameOver();
        }
            
    }
}
