using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyTypes;
    private int numCows;
    private GameObject[] cows;
    public int maxEnemies;
    private int numEnemies = 0;
    public Vector3 spawnBoundaryBottomLeft;
    public Vector3 spawnBoundaryTopRight;
    public static SpawnManager _instance;

    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        cows = GameObject.FindGameObjectsWithTag("Cow");
        numCows = cows.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (numEnemies < maxEnemies)
        {
            numEnemies++;
            int randomEnemy = Random.Range(0, enemyTypes.Length);
            Vector3 trajectory = new Vector3(Random.Range(spawnBoundaryBottomLeft.x, spawnBoundaryTopRight.x), 0, Random.Range(spawnBoundaryBottomLeft.z, spawnBoundaryTopRight.z));
            trajectory.Normalize();
            trajectory = trajectory * 40;
            Instantiate(enemyTypes[randomEnemy], new Vector3(trajectory.x, enemyTypes[randomEnemy].transform.position.y, trajectory.z), Quaternion.identity);
        }
    }

    public void DestroyCow(GameObject cow)
    {
        if (cow != null)
        {
            Destroy(cow);
            numCows--;
        }
    }

    public int CowsRemaining()
    {
        return numCows;
    }

    public GameObject[] ProvideTargetList()
    {
        List<GameObject> potentialTargets = new List<GameObject>();
        GameObject[] targets;

        for(int i = 0; i < cows.Length; i++)
        {
            if(cows[i] != null && !cows[i].GetComponent<CowController>().IsClaimed())
            {
                potentialTargets.Add(cows[i]);
            }
        }

        targets = new GameObject[potentialTargets.Count];

        for(int i = 0; i < potentialTargets.Count; i++)
        {
            targets[i] = potentialTargets[i];
        }
        return targets;
    }

    public void EnemyDestroyed()
    {
        numEnemies--;
    }
}
