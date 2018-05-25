using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BoardManagerBoss : MonoBehaviour
{

    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerwallTiles;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        Debug.Log("Board Manager Boss loaded");

        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerwallTiles[Random.Range(0, outerwallTiles.Length)];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);

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

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum, int level)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            
            if(level == 5)
            {
                GameObject tileChoice = tileArray[0];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
            if (level == 10)
            {
                GameObject tileChoice = tileArray[1];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
            if (level == 15)
            {
                GameObject tileChoice = tileArray[2];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();

        int enemyCount = 1;
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount,level);

        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
