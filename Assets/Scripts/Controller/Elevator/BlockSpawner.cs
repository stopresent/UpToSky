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

    UnityEngine.Collider2D[] collider2Ds = new UnityEngine.Collider2D[4];

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

            Vector3 newPos;

            // TODO
            // newPos ��ó�� ���� �ִ��� üũ�ؾ���.
            while (true)
            {
                newPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);

                if (Physics2D.OverlapCircleNonAlloc(newPos, 100, collider2Ds, LayerMask.GetMask("Block")) == 0)
                    break;
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

        gameObject.transform.position = gameObject.transform.position + new Vector3(0, 8, 0);

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