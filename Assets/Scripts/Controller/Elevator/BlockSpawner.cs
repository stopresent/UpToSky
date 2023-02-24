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

    // 한번 소환되면 Y간격을 두고 소환되게끔
    // 한 자리에서 소환되면 그 자리랑 근처에서는 소환 안되게

    private void Update()
    {
        // 특정 조건을 만족할때만 랜덤한 위치에 블록을 만든다
        // 특정 조건 : Stay를 spawnInterval(2초)이상 할때
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

        // 더 좋은 코드가 없을까..?
        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (PlayerY < (int)Define.Height.City || randRange <= 40)
            {
                GameObject breakableBlock = Managers.Resource.Instantiate("Blocks/SlipBlock");
                breakableBlock.transform.position = newPos;
            }
            else if (PlayerY < (int)Define.Height.City || randRange <= 100)
            {
                GameObject block = Managers.Resource.Instantiate("Blocks/BouncyBlock");
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


        // 스코어 모드에서 블럭 생성
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