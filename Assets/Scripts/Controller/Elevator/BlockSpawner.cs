using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Define;
using UnityEngine.SocialPlatforms.Impl;

public class BlockSpawner : MonoBehaviour
{
    public Vector3 min;
    public Vector3 max;
    public Vector3 newPos;
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

        PlayerY = (int)GameObject.Find("Player").transform.position.y * 3;

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
        string path = "";

        if (Managers.Game.Mode == Define.Mode.StoryMode)
        {
            if (PlayerY < (int)Define.Height.Mountain && randRange <= 30) path = "Blocks/SlipBlock";
            else if (PlayerY < (int)Define.Height.Mountain && randRange <= 100) path = "Blocks/MountainBlock";
            else if (PlayerY < (int)Define.Height.SkyWorld && randRange <= 30) path = "Blocks/FlightBlock";
            else if (PlayerY < (int)Define.Height.SkyWorld && randRange <= 100) path = "Blocks/SkyWorldBlock";
            else if (PlayerY < (int)Define.Height.Stratosphere && randRange <= 15) path = "Blocks/AirBalloonBlock";
            else if (PlayerY < (int)Define.Height.Stratosphere && randRange <= 100) path = "Blocks/StratosphereBlock";
            else if (PlayerY < (int)Define.Height.Thermosphere && randRange <= 30) path = "Blocks/AuroraBlock";
            else if (PlayerY < (int)Define.Height.Thermosphere && randRange <= 100) path = "Blocks/ThermosphereBlock";
            else if (PlayerY <= (int)Define.Height.GalaxyBlues && randRange <= 10) path = "Blocks/BlackHole";
            else path = "Blocks/SpaceBlock";

            GameObject Block = Managers.Resource.Instantiate(path);
            Block.transform.position = newPos;

        }
        if (Managers.Game.Mode == Define.Mode.ScoreMode)
        {

            // ���ھ� ��忡�� �� ����
            if (randRange <= 10)
            {
                GameObject breakableBlock = Managers.Resource.Instantiate("Blocks/BlackHole");
                breakableBlock.transform.position = newPos;
            }
            else if (randRange <= 100)
            {
                GameObject block = Managers.Resource.Instantiate("Blocks/SpaceBlock");
                block.transform.position = newPos;
            }
        }

    }

}