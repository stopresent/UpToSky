using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public float spawnInterval = 0.5f;
    public float prevSpawnTime = 0.0f;
    Vector3 min;
    Vector3 max;
    Vector3 prevSpawnpoint = Vector3.zero;
    Vector2 newPos;

    // 한번 소환되면 Y간격을 두고 소환되게끔
    // 한 자리에서 소환되면 그 자리랑 근처에서는 소환 안되게

    private void Update()
    {
        // 조건
        // maxBlock 개수를 채울때까지 생성
        if (Managers.Game.CurrentBlockCount < Managers.Game.MaxBlockCount)
        {
            // 특정 공간에 블럭이 3~4개 생성하면 엘베가 올라감
            spawnBlock();
        }

        // 특정 조건을 만족할때만 랜덤한 위치에 블록을 만든다
        // 특정 조건 : Stay를 spawnInterval(2초)이상 할때
        //if (getStayingTime() > prevSpawnTime + spawnInterval)
        //    spawnBlock();

    }

    void spawnBlock()
    {
        //prevSpawnTime = getStayingTime();
        //min = GetComponent<BoxCollider2D>().bounds.min;
        //max = GetComponent<BoxCollider2D>().bounds.max;
        //Vector3 newPos;
        //newPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);

        //int randRange = Random.Range(1, 100);

        //if (randRange <= 40)
        //{
        //    GameObject breakableBlock = Managers.Resource.Instantiate("BreakableBlock");
        //    breakableBlock.transform.position = newPos;
        //    _blockCount++;

        //}
        //else if (randRange <= 100)
        //{
        //    GameObject block = Managers.Resource.Instantiate("Block");
        //    block.transform.position = newPos;
        //    _blockCount++;

        //}

        //prevSpawnpoint = newPos;

        for (int i = 0; i < 4; i++)
        {
            min = GetComponent<BoxCollider2D>().bounds.min;
            max = GetComponent<BoxCollider2D>().bounds.max;

            // TODO
            // newPos 근처에 블럭이 있는지 체크해야함.
            // 진짜 열심히 생각했는데 모르겠다...
            while (true)
            {
                newPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));

                OnTriggerEnter2D onTriggerEnter2D = new OnTriggerEnter2D();
                onTriggerEnter2D.position = newPos;
                Collider2D collider2D =  Physics2D.OverlapCircle(newPos, 1.5f, LayerMask.GetMask("Block"));
                if (!collider2D)
                {
                    Debug.Log("Not Overlap");
                    break;
                }
                else
                {
                    Debug.Log($"Overlap {newPos}, {collider2D.name} 위치 재설정 필요");
                }
            }

            int randRange = Random.Range(1, 100);

            if (randRange <= 40)
            {
                GameObject breakableBlock = Managers.Resource.Instantiate("BreakableBlock");
                breakableBlock.transform.position = newPos;
                Managers.Game.CurrentBlockCount++;

            }
            else if (randRange <= 100)
            {
                GameObject block = Managers.Resource.Instantiate("Block");
                block.transform.position = newPos;
                Managers.Game.CurrentBlockCount++;

            }
        }

        gameObject.transform.position = gameObject.transform.position + new Vector3(0, 7.5f, 0);

    }

    bool CheckNearBlock()
    {
        Vector3 newPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);

        return true;
    }

    float getStayingTime()
    {
        return transform.parent.GetComponentInChildren<Sensor>().stayingTime;
    }

}