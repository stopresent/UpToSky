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

        // 스토리 모드에서 초기 블럭 생성
        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            GameObject Block = Managers.Resource.Instantiate("Blocks/MountainBlock");
            Block.transform.position = newPos;
        }

        // 스코어 모드에서 초기 블럭 생성
        if (Managers.Game.Mode == Define.Mode.ScoreMode)
        {
            GameObject block = Managers.Resource.Instantiate("Blocks/SpaceBlock");
            block.transform.position = newPos;

        }
    }

}
