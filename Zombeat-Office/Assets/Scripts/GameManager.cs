using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .1f;
    public static GameManager instance = null;
    public int playerLifePoints = 10;
    [HideInInspector] public bool playersTurn = true;
    [HideInInspector] public BoardManager boardScripts;
    [HideInInspector] public BoardManagerBoss boardBossScripts;

    // Rythm management section
    public GameObject playerObject;
    private Player player;
    public RhythmEventProvider eventProvider;
    private int halfPeriodFrames = 0;
    private int frameCount = 0;
    private bool countFrames = false;
    private bool actionTime = false;


    private Text levelText;
    private GameObject levelImage;
    [HideInInspector]public int level = 0;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;

    private RhythmTool rTool;
    private AudioClip audioClip;

    // Use this for initialization
    void Awake()
    {
        rTool = GetComponent<RhythmTool>();
        audioClip = GetComponent<AudioClip>();
        audioClip = GetComponent<AudioSource>().clip;

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        rTool.SongLoaded += OnSongLoaded;
        rTool.audioClip = audioClip;

        player = playerObject.GetComponent<Player>();
        eventProvider.Beat += initializer;

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScripts = GetComponent<BoardManager>();
        boardBossScripts = GetComponent<BoardManagerBoss>();
        InitGame();
    }

    private void OnSongLoaded() {
      rTool.Play();
    }

    private void OnLevelWasLoaded(int index)
    {
        level++;
        Debug.Log("level : " + level + " successfully loaded");
        InitGame();
    }

    /*void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {

        InitGame();
        Debug.Log("level : " + level + " successfully loaded");
        Debug.Log(scene);
        level++;
    }*/



    void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Floor " + (level);
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);

        enemies.Clear();

        if (level % 5== 0 )
            boardBossScripts.SetupScene(level);
        else
            boardScripts.SetupScene(level);
    }

    private void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "You survived " + (level+1) + "floors";
        levelImage.SetActive(true);
        enabled = false;
        rTool.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if ( playersTurn || enemiesMoving || doingSetup)
            return;

        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy script)
    {
        enemies.Add(script);
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
        if ( enemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }

        playersTurn = true;
        enemiesMoving = false;
    }


    // To remove events
    void onDestroy ()
    {
        eventProvider.SubBeat -= onSubBeat;
    }

    private void initializer (Beat beat)
    {
        eventProvider.FrameChanged += initFrameCounter;
        eventProvider.SubBeat += initHalfQuarterBeat;
    }


    // This method is called only during the first quarter beat of the song
    private void initHalfQuarterBeat(Beat beat, int beatIndex)
    {
        eventProvider.FrameChanged -= initFrameCounter;
        eventProvider.SubBeat -= initHalfQuarterBeat;
        eventProvider.Beat -= initializer;

        eventProvider.SubBeat += onSubBeat;
        eventProvider.FrameChanged += onFrameChanged;

        halfPeriodFrames = halfPeriodFrames / 2;

        Debug.Log("halfPeriodFrames = " + halfPeriodFrames);
    }


    private void initFrameCounter(int a, int b)
    {
        halfPeriodFrames += 1;
    }


    // Used for counting frames in the action period
    private void onFrameChanged (int a, int b) // Do not know what a and b are, but seems to work (update: nothing better after checking official documentation)
    {
        if (countFrames)
        {
            frameCount += 1;

            // Checking if the number of frames counted corresponds to the beginning of the action period
            if (frameCount == halfPeriodFrames)
            {
                actionTime = true;
                player.actionPeriod = true;
                player.hasMoved = false;
            }

            // Checking if the number of frames counted corresponds to the end of the action period
            else if (frameCount == 2 * halfPeriodFrames)
            {
                actionTime = false;
                player.actionPeriod = false;
                //player.hasMoved = false;
                frameCount = 0;
            }
        }
    }


    private void onSubBeat(Beat beat, int beatIndex)
    {
        // Checking if we are in the 3rd (and last) quarter beat, if so starting to count beats
        if (beatIndex == 3)
        {
            countFrames = true;
        }
    }
}
