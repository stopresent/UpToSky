using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;
using UnityEngine.SocialPlatforms.Impl;

public class BlockSpawner : MonoBehaviour
{
    Vector3 min;
    Vector3 max;
    Vector3 newPos;
    public bool Spawn = false;
    public int PlayerY;

    // �ѹ� ��ȯ�Ǹ� Y������ �ΰ� ��ȯ�ǰԲ�
    // �� �ڸ����� ��ȯ�Ǹ� �� �ڸ��� ��ó������ ��ȯ �ȵǰ�

    private void Update()
    {
        // Ư�� ������ �����Ҷ��� ������ ��ġ�� ����� �����
        // Ư�� ���� : Stay�� spawnInterval(2��)�̻� �Ҷ�
        if(Spawn == true)
            spawnBlock();

        PlayerY = (int)GameObject.Find("Player").transform.position.y;

    }

    void spawnBlock()
    {
        min = GetComponent<BoxCollider2D>().bounds.min;
        max = GetComponent<BoxCollider2D>().bounds.max;
        newPos = new Vector3(Random.Range(min.x, max.x), Random.Range(min.y, max.y), 0);

        SelectBlock(PlayerY);

        Spawn = false;
    }

    void SelectBlock(int height)
    {
        int randRange = Random.Range(1, 100);

        switch (randRange)
        {
            default:
                break;
        }

        // �� ���� �ڵ尡 ������..?
        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (PlayerY < (int)Define.Height.City || randRange <= 40)
            {
                GameObject breakableBlock = Managers.Resource.Instantiate("Blocks/BreakableBlock");
                breakableBlock.transform.position = newPos;
            }
            else if (PlayerY < (int)Define.Height.City || randRange <= 100)
            {
                GameObject block = Managers.Resource.Instantiate("Blocks/Block");
                block.transform.position = newPos;
            }
            else if (PlayerY < (int)Define.Height.Mountain || randRange <= 40)
            {
                
            }
            else if (PlayerY < (int)Define.Height.Mountain || randRange <= 100)
            {

            }
            else if (PlayerY < (int)Define.Height.SkyWorld || randRange <= 40)
            {
                
            }
            else if (PlayerY < (int)Define.Height.SkyWorld || randRange <= 100)
            {

            }
            else if (PlayerY < (int)Define.Height.Stratosphere || randRange <= 40)
            {
                
            }
            else if (PlayerY < (int)Define.Height.Stratosphere || randRange <= 100)
            {

            }
            else if (PlayerY < (int)Define.Height.Thermosphere || randRange <= 40)
            {
                
            }
            else if (PlayerY < (int)Define.Height.Thermosphere || randRange <= 100)
            {

            }
            else if (PlayerY <= (int)Define.Height.GalaxyBlues || randRange <= 40)
            {
                
            }
            else
            {
                
            }
        }


        // ���ھ� ��忡�� �� ����
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