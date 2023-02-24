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

        // ���丮 ��忡�� �ʱ� �� ����
        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (randRange <= 40)
            {
                GameObject breakableBlock = Managers.Resource.Instantiate("Blocks/SlipBlock");
                breakableBlock.transform.position = newPos;

            }
            else if (randRange <= 100)
            {
                GameObject block = Managers.Resource.Instantiate("Blocks/BouncyBlock");
                block.transform.position = newPos;
            }
        }

        // ���ھ� ��忡�� �ʱ� �� ����
        if (Managers.Game.Mode == Define.Mode.ScoreMode)
        {
            if (randRange <= 40)
            {
                GameObject breakableBlock = Managers.Resource.Instantiate("Blocks/BreakableBlock");
                breakableBlock.transform.position = newPos;

            }
            else if (randRange <= 100)
            {
                GameObject block = Managers.Resource.Instantiate("Blocks/Block");
                block.transform.position = newPos;
            }
        }
    }

}
