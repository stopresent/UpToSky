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
        // Ư�� ������ �����Ҷ��� ������ ��ġ�� ����� �����
        // Ư�� ���� : Stay�� spawnInterval(2��)�̻� �Ҷ�
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