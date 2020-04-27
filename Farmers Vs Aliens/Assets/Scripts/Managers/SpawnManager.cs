using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyTypes;
    public int maxEnemies;
    public Vector3 spawnBoundaryBottomLeft;
    public Vector3 spawnBoundaryTopRight;
    public int spawnWaves;
    public int enemiesPerWave;
    public static SpawnManager _instance;
    private int currentWave = 0;
    private int numCows;
    private GameObject[] cows;
    private int numEnemies = 0;
    private int waveCount = 0;
    private bool active = false;

    private void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        cows = GameObject.FindGameObjectsWithTag("Cow");
        numCows = cows.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            SpawnWave();
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

    public bool NoMoreCows()
    {
        if(CowsRemaining() == 0)
        {
            return true;
        }
        return false;
    }

    public void EnemyDestroyed()
    {
        numEnemies--;
    }

    private void SpawnWave()
    {
        if (numEnemies < maxEnemies && waveCount < enemiesPerWave)
        {
            numEnemies++;
            waveCount++;
            int randomEnemy = Random.Range(0, enemyTypes.Length);
            Vector3 trajectory = new Vector3(Random.Range(spawnBoundaryBottomLeft.x, spawnBoundaryTopRight.x), 0, Random.Range(spawnBoundaryBottomLeft.z, spawnBoundaryTopRight.z));
            trajectory.Normalize();
            trajectory = trajectory * 40;
            Instantiate(enemyTypes[randomEnemy], new Vector3(trajectory.x, enemyTypes[randomEnemy].transform.position.y, trajectory.z), Quaternion.identity);
        }
        else if(waveCount >= enemiesPerWave)
        {
            active = false;
            currentWave++;
            maxEnemies++;
            enemiesPerWave += currentWave * 2;
        }
    }

    public void StartWave()
    {
        waveCount = 0;
        numEnemies = 0;
        active = true;
    }

    public bool WaveFinished()
    {
        if(!active && numEnemies <= 0)
        {
            return true;
        }
        return false;
    }

    public bool NoMoreWaves()
    {
        if(WaveFinished() && currentWave >= spawnWaves)
        {
            return true;
        }

        return false;
    }
}
