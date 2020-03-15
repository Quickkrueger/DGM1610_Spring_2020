using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //TODO: Add ability to spawn more than just one type of enemy.
    public GameObject[] enemyPrefabs;
    public int spawnLimit = 5;
    int enemiesSpawned = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            int functionPicker = Random.Range(0, 3);
            if(functionPicker == 0)
            {
                SpawnWithFor();//Only adds five enemies if there are not already five
            }
            else if(functionPicker == 1)
            {
                SpawnWithWhile();//Only adds five enemies if there are not already five
            }
            else
            {
                SpawnWithDoWhile();//Will always add at least one enemy, but can add up to five if causinging the initial spawn;
            }
        }
    }

    private void SpawnWithFor()
    {
        for (int i = enemiesSpawned; enemiesSpawned < spawnLimit; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector3(0, 0, 0), enemyPrefabs[enemyIndex].transform.rotation);
            enemiesSpawned = i + 1;
        }
    }

    private void SpawnWithWhile()
    {
        while(enemiesSpawned < spawnLimit)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector3(0, 0, 0), enemyPrefabs[enemyIndex].transform.rotation);
            enemiesSpawned++;
        }
    }

    private void SpawnWithDoWhile()
    {
        enemiesSpawned++;
        do
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], new Vector3(0, 0, 0), enemyPrefabs[enemyIndex].transform.rotation);
            enemiesSpawned++;

        } while (enemiesSpawned < spawnLimit);
    }
}
