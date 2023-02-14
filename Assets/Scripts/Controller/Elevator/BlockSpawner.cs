using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public float spawnInterval = 1.0f;
    public float prevSpawnTime = 0.0f;
    Vector3 min;
    Vector3 max;
    Vector3 prevSpawnpoint = Vector3.zero;

    // �ѹ� ��ȯ�Ǹ� Y������ �ΰ� ��ȯ�ǰԲ�
    // �� �ڸ����� ��ȯ�Ǹ� �� �ڸ��� ��ó������ ��ȯ �ȵǰ�

    private void Update()
    {
        // Ư�� ������ �����Ҷ��� ������ ��ġ�� ����� �����
        // Ư�� ���� : Stay�� spawnInterval(2��)�̻� �Ҷ�
        if (getStayingTime() > prevSpawnTime + spawnInterval)
            spawnBlock();

    }

    void spawnBlock()
    {
        prevSpawnTime = getStayingTime();
        min = GetComponent<BoxCollider2D>().bounds.min;
        max = GetComponent<BoxCollider2D>().bounds.max;
        Vector3 newPos;
        newPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);
                
        GameObject newBlock = Managers.Resource.Instantiate("Block");
        newBlock.transform.position = newPos;
                
        prevSpawnpoint = newPos;
    }

    float getStayingTime()
    {
        return transform.parent.GetComponentInChildren<Sensor>().stayingTime;
    }
}