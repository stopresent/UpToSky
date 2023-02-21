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

    // �ѹ� ��ȯ�Ǹ� Y������ �ΰ� ��ȯ�ǰԲ�
    // �� �ڸ����� ��ȯ�Ǹ� �� �ڸ��� ��ó������ ��ȯ �ȵǰ�

    private void Update()
    {
        // ����
        // maxBlock ������ ä�ﶧ���� ����
        if (Managers.Game.CurrentBlockCount < Managers.Game.MaxBlockCount)
        {
            // Ư�� ������ ���� 3~4�� �����ϸ� ������ �ö�
            spawnBlock();
        }

        // Ư�� ������ �����Ҷ��� ������ ��ġ�� ����� �����
        // Ư�� ���� : Stay�� spawnInterval(2��)�̻� �Ҷ�
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
            // newPos ��ó�� ���� �ִ��� üũ�ؾ���.
            // ��¥ ������ �����ߴµ� �𸣰ڴ�...
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
                    Debug.Log($"Overlap {newPos}, {collider2D.name} ��ġ �缳�� �ʿ�");
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