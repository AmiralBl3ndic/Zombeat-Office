using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public BoardManager boardScripts;

    private int level = 3;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScripts = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        boardScripts.SetupScene(level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}