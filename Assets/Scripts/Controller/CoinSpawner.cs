using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // this is attached to "Block" Prefab
    int now;
    int coinSpawnChance = 10;

    private void Start()
    {
        // �÷��� �ð��� ���� ���� ��� ���� Ȯ���� �����Ѵ�
        now = (int) GameObject.Find("UI_Game").GetComponent<UI_Game>().PlayTime;
        spawnCoin(coinSpawnChance + now / 10); // �ʴ� ��� ���� Ȯ���� 0.1�۾� ����Ѵ�
    }

    public void spawnCoin(int chance)
    {
        if(Random.Range(0, 100) < chance)
        {
            GameObject coin = Managers.Resource.Instantiate("Coin");
            coin.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }
}