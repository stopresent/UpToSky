using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // this is attached to "Block" Prefab
    int now;
    int coinSpawnChance = 10;   // 기본 확률 10퍼

    private void Start()
    {
        // 플레이 시간에 따라 점점 골드 스폰 확률이 증가한다
        now = (int) GameObject.Find("UI_Game").GetComponent<UI_Game>().PlayTime;
        StartCoroutine(spawnCoin(coinSpawnChance + now / 10));    // 초당 골드 스폰 확률이 0.1퍼씩 상승한다
    }

    IEnumerator spawnCoin(float chance)
    {
        yield return new WaitForSeconds(0.00001f);

        if(Random.Range(0, 100) < chance)
        {
            GameObject coin = Managers.Resource.Instantiate("Coin");
            coin.transform.position = transform.position + new Vector3(0, 0.5f, 0);
        }
    }
}