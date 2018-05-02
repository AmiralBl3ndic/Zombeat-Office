using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScripts;

    private int level = 3;

    // Use this for initialization
    void Awake()
    {
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