using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = .01f;
    public static GameManager instance = null;
    public int playerLifePoints = 10;
    [HideInInspector] public bool playersTurn = true;
    [HideInInspector] public BoardManager boardScripts;
    [HideInInspector] public BoardManagerBoss boardBossScripts;

    // Rythm management section
    public GameObject playerObject;
    private Player player;
    public RhythmEventProvider eventProvider;
    private RhythmTool rTool;
    private AudioClip audioClip;
    private int beatIndex = 0;
    private int counter = 0;
    private bool wtfToggle = false;


    private Text levelText;
    private GameObject levelImage;
    [HideInInspector]public int level = 0;
    private List<Enemy> enemies;
    private bool enemiesMoving;
    private bool doingSetup;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        // Getting RhythmTool needed components
        rTool = GetComponent<RhythmTool>();
        audioClip = GetComponent<AudioSource>().clip;
        rTool.audioClip = audioClip;
        rTool.SongLoaded += OnSongLoaded;
        eventProvider.SubBeat += onSubBeat;

        player = playerObject.GetComponent<Player>();

        DontDestroyOnLoad(gameObject);
        enemies = new List<Enemy>();
        boardScripts = GetComponent<BoardManager>();
        boardBossScripts = GetComponent<BoardManagerBoss>();
        InitGame();
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
            if (enemies[i] == null)
            {
                yield return new WaitForSeconds(enemies[i].moveTime);
                continue;
            }

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


    private void OnSongLoaded()
    {
        rTool.Play();
    }


    private void onSubBeat(Beat beat, int index)
    {
        /*if (index == 1)
        {
            player.actionPeriod = false;
            player.hasMoved = false;
            //Debug.Log("Window closed");
        }

        if (index == 3)
        {
            player.actionPeriod = true;
            player.hasMoved = false;
            //Debug.Log("Window opened");
        }*/

        if (index == 1 || index == 3)
        {
            player.actionPeriod = !player.actionPeriod;
            player.hasMoved = false;
        }

        /*if (player.actionPeriod == false)
            Debug.Log("Not inside action period");
        else
            Debug.Log("Inside action period");*/
    }
}
