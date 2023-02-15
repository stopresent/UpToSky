using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    Transform[] spawnPoints;
    public float spawnInterval = 0.5f;
    public float prevSpawnTime = 0.0f;
    public int spawnCount;

    private void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();
        spawnCount = spawnPoints.Length;
    }

    private void Update()
    {
        // 특정 조건을 만족할때만 랜덤한 위치에 블록을 만든다
        // 특정 조건 : Stay를 spawnInterval(2초)이상 할때
        if (getStayingTime() > prevSpawnTime + spawnInterval)
            spawnBlock();
    }

    float getStayingTime()
    {
        return transform.parent.GetComponentInChildren<Sensor>().stayingTime;
    }

    void spawnBlock()
    {
        prevSpawnTime = getStayingTime();

        int point = Random.Range(1, spawnCount);

        int randRange = Random.Range(1, 100);

        if (randRange <= 40)
        {
            GameObject breakableBlock = Managers.Resource.Instantiate("BreakableBlock");
            breakableBlock.transform.position = spawnPoints[point].position;

        }
        else if (randRange <= 100)
        {
            GameObject block = Managers.Resource.Instantiate("Block");
            block.transform.position = spawnPoints[point].position;

        }

    }
}