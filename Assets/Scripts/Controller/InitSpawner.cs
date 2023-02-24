using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpawner : MonoBehaviour
{
    BoxCollider2D area;
    Vector2 max;
    Vector2 min;
    float yInterval;
    int FloorNum = 4;


    //private void Start()
    //{
    //    Vector3 TestPos1 = new Vector3(0, 1, 0);
    //    Vector3 TestPos2 = new Vector3(1, 1, 0);

    //    GameObject block1 = Managers.Resource.Instantiate("Block");
    //    block1.transform.position = TestPos1;

    //    GameObject block2 = Managers.Resource.Instantiate("Block");
    //    block2.transform.position = TestPos2;

    //}

    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        max = area.bounds.max;
        min = area.bounds.min;
        yInterval = (max.y - min.y) / FloorNum;

        for (int i = 1; i <= FloorNum; i++)
            SpawnBlock(i);
    }

    void SpawnBlock(int floor)
    {
        float newPosY = Random.Range(min.y + yInterval * (floor - 1), min.y + yInterval * floor);
        Vector3 newPos = new Vector3(Random.Range(min.x, max.x), newPosY, 0);

        int randRange = Random.Range(1, 100);

        if (randRange <= 40)
        {
            GameObject breakableBlock = Managers.Resource.Instantiate("BreakableBlock");
            breakableBlock.transform.position = newPos;

        }
        else if (randRange <= 100)
        {
            GameObject block = Managers.Resource.Instantiate("Block");
            block.transform.position = newPos;
        }
    }

}
