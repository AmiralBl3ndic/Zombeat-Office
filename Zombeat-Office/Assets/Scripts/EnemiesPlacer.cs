using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPlacer : MonoBehaviour {

    public List<GameObject> enemies;
    public GameObject enabledEnemiesHolder;

    public int minimumToSpawn = 1;
    public int maximumToSpawn = 2;

    public bool preciseNumber = false;
    public int enemiesToSpawn = 0;
    
	void Start () {
        for (int i = 0; i < enemies.Count; i++)
            enemies[i].SetActive(false);

        SetEnemies();

        GameObject.Find("PotentialEnemies").SetActive(false);
        enabledEnemiesHolder.SetActive(true);
	}


    private void SetEnemies()
    {
        int toSpawn = enemiesToSpawn;
        if (!preciseNumber)
            toSpawn = RandIntInRange(minimumToSpawn, maximumToSpawn);

        if (toSpawn > enemies.Count)
        {
            Debug.LogError(string.Format("{0} enemies to spawn but there are only {1} potential enemies", toSpawn, enemies.Count));
            return;
        }

        for (int i = 0; i < toSpawn; i++)
        {
            int randomEnemyID = RandIntInRange(0, enemies.Count - 1);

            enemies[randomEnemyID].transform.SetParent(enabledEnemiesHolder.transform);
            enemies[randomEnemyID].SetActive(true);

            enemies.RemoveAt(randomEnemyID);
        }
    }


    private int RandIntInRange(int min, int max)
    {
        if (min == max)
            return min;
        else if (min < max)
            return Mathf.CeilToInt(Random.Range(min, max + 1));
        else
            return Mathf.CeilToInt(Random.Range(max, min + 1));
    }
}
