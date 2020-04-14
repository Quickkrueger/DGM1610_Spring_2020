using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public int maxEnemies;
    public Vector3 spawnBoundaryBottomLeft;
    public Vector3 spawnBoundaryTopRight;
    private List<GameObject> currentEnemies;

    void Start()
    {
        currentEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentEnemies.Count < maxEnemies)
        {
            int randomEnemy = Random.Range(0, enemyTypes.Length);
            Vector3 trajectory = new Vector3(Random.Range(spawnBoundaryBottomLeft.x, spawnBoundaryTopRight.x), 0, Random.Range(spawnBoundaryBottomLeft.z, spawnBoundaryTopRight.z));
            trajectory.Normalize();
            trajectory = trajectory * 40;
            currentEnemies.Add(Instantiate(enemyTypes[randomEnemy], new Vector3(trajectory.x, enemyTypes[randomEnemy].transform.position.y, trajectory.z), Quaternion.identity));
        }
    }
}
