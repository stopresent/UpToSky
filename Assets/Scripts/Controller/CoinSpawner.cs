using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // this is attached to "Block" Prefab

    private void Start()
    {
        spawnCoin(0.1f);        // 10퍼센트의 확률로 골드가 스폰
    }

    public void spawnCoin(float chance)
    {
        if(Random.Range(0.0f, 1.0f) < chance)
        {
            GameObject coin = Managers.Resource.Instantiate("Coin");
            coin.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }
}