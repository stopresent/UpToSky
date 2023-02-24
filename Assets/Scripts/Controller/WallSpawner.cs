using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    float Height;
    float HInterval;
    float prevSpawnH = 0.0f;

    private void Start()
    {
        SetHInterval();
    }

    private void Update()
    {
        Height = GameObject.Find("Player").transform.position.y;

        if (Height > HInterval + prevSpawnH)
        {
            prevSpawnH = Height;
            SetHInterval();
            spawnBlock();
        }

    }

    void SetHInterval()
    {
        HInterval = Random.Range(7.0f, 14.0f);
    }

    void spawnBlock()
    {
        int num = Random.Range(0, 2);
        int wallNum = Random.Range(1, 5);
        GameObject wall = Managers.Resource.Instantiate($"Wall{wallNum}");
        wall.transform.position = transform.GetChild(num).position;
    }
}
