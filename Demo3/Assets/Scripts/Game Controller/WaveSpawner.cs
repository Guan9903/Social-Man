using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Enemy[] enemyType;
        //public int count;
        public float rate;
    }

    public Wave[] waves;
    public Transform[] spawnPoints;
    public float wavesInterval = 5f;        //波数之间的间隔

    public static bool endGame;
    
    SpawnState state = SpawnState.COUNTING;
    int nextWave = 0;
    float waveCountdown;
    float searchCountdown = 1f;

    // Start is called before the first frame update
    void Start()
    {
        endGame = false;
        waveCountdown = wavesInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //检测还有多少敌人生存
            if (!EnemyIsAlive())
            {
                //开始新一轮
                WaveCompleted();
                //return;
            }
            else
                return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                //开始生成敌人波
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnState.COUNTING;
        waveCountdown = wavesInterval;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            endGame = true;
            Debug.Log("All Waves Complete");
        }
        else
            nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
                return false;
        }

        return true;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        Debug.Log("Spawning Wave: " + wave.name);
        state = SpawnState.SPAWNING;

        //生成
        //for (int i = 0; i < wave.count; i++)
        //{
        //    SpawnEnemy(wave.enemyType.transform);
        //    yield return new WaitForSeconds(1f / wave.rate);
        //}

        for (int i = 0; i < wave.enemyType.Length; i++)
        {
            for (int j = 0; j < wave.enemyType[i].amount; j++)
            {
                SpawnEnemy(wave.enemyType[i].transform);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform enemy)
    {
        //生成敌人
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemy, sp.position, sp.rotation);
    }
}
